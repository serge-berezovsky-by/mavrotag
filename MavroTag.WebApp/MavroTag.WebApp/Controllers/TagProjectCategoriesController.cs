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
    public class TagProjectCategoriesController : BaseController
    {
        ITagProjectService _tagProjectService;
        ITagProjectUserPermissionService _tagProjectUserPermissionService;
        IUserService _userService;

        public TagProjectCategoriesController(ITagProjectService tagProjectService,
            ITagProjectUserPermissionService tagProjectUserPermissionService,
            IUserService userService)
        {
            _tagProjectService = tagProjectService;
            _tagProjectUserPermissionService = tagProjectUserPermissionService;
            _userService = userService;
        }

        public ActionResult Index(long tagProjectId)
        {
            CheckPermissions(tagProjectId, out var tagProject);

            var tagProjectUserPermissions = _tagProjectUserPermissionService.GetAll()
                .Where(c => c.TagProjectId == tagProjectId)
                .ToList();

            var model = new TagProjectCategoriesModel()
            {
                TagProjectId = tagProjectId,
                TagProjectName = tagProject.Name,
                TagProjectCategories = new List<TagProjectCategoryModel>()
            };

            // TODO: get categories

            return View(model);
        }

        private void CheckPermissions(long tagProjectId, out TagProject tagProject)
        {
            var tagProjectUserPermissions = _tagProjectUserPermissionService
                .GetAll().Where(c => c.UserId == AuthHelper.UserId && c.TagProjectId == tagProjectId).ToList();

            if (!tagProjectUserPermissions.Any()) AuthHelper.LogoutAndRedirect();
            if( !TagProjectUserPermissionModel
                .FromTagProjectUserPermissions(AuthHelper.User, tagProjectUserPermissions)
                .Contains(TagProjectPermissions.ManageTexts)) AuthHelper.LogoutAndRedirect();

            tagProject = _tagProjectService.GetById(tagProjectId);
        }
    }
}