using AutoMapper;
using MavroTag.Core.Data;
using MavroTag.Core.Domain;
using MavroTag.Core.Enums;
using MavroTag.Core.Interfaces;
using MavroTag.WebApp.Helpers;
using MavroTag.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MavroTag.WebApp.Controllers
{
    public class TagProjectTextsController : BaseController
    {
        ITagProjectService _tagProjectService;
        ITagProjectUserPermissionService _tagProjectUserPermissionService;
        IUserService _userService;
        ITagProjectCategoryService _tagProjectCategoryService;
        ITagProjectTextService _tagProjectTextService;

        public TagProjectTextsController(ITagProjectService tagProjectService,
            ITagProjectUserPermissionService tagProjectUserPermissionService,
            IUserService userService,
            ITagProjectCategoryService tagProjectCategoryService,
            ITagProjectTextService tagProjectTextService)
        {
            _tagProjectService = tagProjectService;
            _tagProjectUserPermissionService = tagProjectUserPermissionService;
            _userService = userService;
            _tagProjectCategoryService = tagProjectCategoryService;
            _tagProjectTextService = tagProjectTextService;
        }

        public ActionResult Index(long tagProjectCategoryId)
        {
            CheckPermissions(tagProjectCategoryId, out var tagProjectCategory, out var tagProject);

            var model = new TagProjectTextsModel()
            {
                TagProjectId = tagProject.Id,
                TagProjectName = tagProject.Name,
                TagProjectCategoryId = tagProjectCategory.Id,
                TagProjectCategoryName = tagProjectCategory.Name,
                TagProjectTexts = new List<TagProjectTextModel>()
            };

            model.TagProjectTexts = _tagProjectTextService.GetByCategoryId(tagProjectCategory.Id)
                .Select(c => TagProjectTextModel.FromTagProjectText(c)).ToList();

            return View(model);
        }

        private void CheckPermissions(long tagProjectCategoryId, out TagProjectCategory tagProjectCategory, out TagProject tagProject)
        {
            tagProjectCategory = _tagProjectCategoryService.GetById(tagProjectCategoryId);
            if (tagProjectCategory == null) AuthHelper.LogoutAndRedirect();

            var tagProjectId = tagProjectCategory.TagProjectId;

            var tagProjectUserPermissions = _tagProjectUserPermissionService
                .GetAll().Where(c => c.UserId == AuthHelper.UserId && c.TagProjectId == tagProjectId).ToList();

            if (!tagProjectUserPermissions.Any()) AuthHelper.LogoutAndRedirect();
            if (!TagProjectUserPermissionModel
                .FromTagProjectUserPermissions(AuthHelper.User, tagProjectUserPermissions)
                .Contains(TagProjectPermissions.ManageTexts)) AuthHelper.LogoutAndRedirect();

            tagProject = _tagProjectService.GetById(tagProjectId);
        }
    }
}