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
    public class TagProjectUserPermissionsController : BaseController
    {
        ITagProjectService _tagProjectService;
        ITagProjectUserPermissionService _tagProjectUserPermissionService;

        public TagProjectUserPermissionsController(ITagProjectService tagProjectService,
            ITagProjectUserPermissionService tagProjectUserPermissionService)
        {
            _tagProjectService = tagProjectService;
            _tagProjectUserPermissionService = tagProjectUserPermissionService;
        }

        public ActionResult Index(long tagProjectId)
        {
            AuthHelper.Check(Permissions.MyTagProjects);
            var tagProject = _tagProjectService.GetById(tagProjectId);
            if (tagProject == null) AuthHelper.LogoutAndRedirect();
            if (tagProject.UserId != AuthHelper.UserId) AuthHelper.LogoutAndRedirect();

            var tagProjectUserPermissions = _tagProjectUserPermissionService.GetAll()
                .Where(c => c.TagProjectId == tagProjectId)
                .ToList();

            var model = new TagProjectUserPermissionsModel()
            {
                TagProjectId = tagProjectId,
                TagProjectName = tagProject.Name,
                TagProjectUserPermissions = new List<TagProjectUserPermissionModel>()
            };

            foreach(var user in tagProjectUserPermissions.Select(c => c.User).Distinct().ToList())
            {
                var tagProjectUserPermissionModel = TagProjectUserPermissionModel
                    .FromTagProjectUserPermissions(user, tagProjectUserPermissions.Where(c => c.User == user));
                model.TagProjectUserPermissions.Add(tagProjectUserPermissionModel);
            }

            return View(model);
        }
    }
}