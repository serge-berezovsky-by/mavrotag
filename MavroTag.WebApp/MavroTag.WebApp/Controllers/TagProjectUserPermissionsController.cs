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
        IUserService _userService;

        public TagProjectUserPermissionsController(ITagProjectService tagProjectService,
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

            var model = new TagProjectUserPermissionsModel()
            {
                TagProjectId = tagProjectId,
                TagProjectName = tagProject.Name,
                TagProjectUserPermissions = new List<TagProjectUserPermissionModel>()
            };

            foreach (var user in tagProjectUserPermissions.Select(c => c.User).Distinct().ToList())
            {
                var tagProjectUserPermissionModel = TagProjectUserPermissionModel
                    .FromTagProjectUserPermissions(user, tagProjectUserPermissions.Where(c => c.User == user));
                tagProjectUserPermissionModel.TagProjectId = tagProjectId;
                model.TagProjectUserPermissions.Add(tagProjectUserPermissionModel);
            }

            return View(model);
        }

        private void CheckPermissions(long tagProjectId, out TagProject tagProject)
        {
            AuthHelper.Check(Permissions.MyTagProjects);
            tagProject = _tagProjectService.GetById(tagProjectId);
            if (tagProject == null) AuthHelper.LogoutAndRedirect();
            if (tagProject.UserId != AuthHelper.UserId) AuthHelper.LogoutAndRedirect();
        }

        public ActionResult Add(long tagProjectId)
        {
            CheckPermissions(tagProjectId, out var tagProject);

            var model = TagProjectUserPermissionModel.FromTagProjectUserPermissions(new Core.Domain.User(), new List<TagProjectUserPermission>());
            model.TagProjectId = tagProjectId;

            return View("Edit", model);
        }

        public ActionResult Edit(long tagProjectId, long userId)
        {
            CheckPermissions(tagProjectId, out var tagProject);
            var user = _userService.GetById(userId);
            if (user == null) AuthHelper.LogoutAndRedirect();

            var tagProjectUserPermissions = _tagProjectUserPermissionService.GetAll()
                .Where(c => c.TagProjectId == tagProject.Id && c.UserId == user.Id)
                .ToList();

            var tagProjectUserPermissionModel = TagProjectUserPermissionModel.FromTagProjectUserPermissions(user, tagProjectUserPermissions);
            tagProjectUserPermissionModel.TagProjectId = tagProjectId;

            var model = tagProjectUserPermissionModel;

            return View("Edit", model);
        }

        public ActionResult Save(TagProjectUserPermissionModel model)
        {
            CheckPermissions(model.TagProjectId, out var tagProject);

            try
            {
                ValidateTagProjectUserPermissionModel(model, out var user);

                ModelState.Clear();

                _tagProjectUserPermissionService.Update(tagProject.Id, user.Id, model.SelectedPermissions);

                var tagProjectUserPermissions = _tagProjectUserPermissionService.GetAll()
                    .Where(c => c.TagProjectId == tagProject.Id && c.UserId == user.Id)
                    .ToList();

                var tagProjectUserPermissionModel = TagProjectUserPermissionModel.FromTagProjectUserPermissions(user, tagProjectUserPermissions);
                tagProjectUserPermissionModel.TagProjectId = model.TagProjectId;

                model = tagProjectUserPermissionModel;
                model.Success = "Пользователь Tag-проекта сохранён.";

                return View("Edit", model);
            }
            catch (Exception e)
            {
                _logger.Error("Save", e);
                model.Error = e.Message;
                return View("Edit", model);
            }
        }

        public ActionResult Delete(long tagProjectId, long userId)
        {
            CheckPermissions(tagProjectId, out var tagProject);
            var user = _userService.GetById(userId);
            if (user == null) AuthHelper.LogoutAndRedirect();

            var model = new TagProjectUserPermissionModel();

            try
            {
                _tagProjectUserPermissionService.Update(tagProject.Id, user.Id, new List<string>());

                model.Success = $"Пользователь {user.Name} Tag-проекта {tagProject.Name} удалён";

                return View("Delete", model);
            }
            catch (Exception e)
            {
                _logger.Error("Delete", e);
                model.Error = e.Message;
                return View("Delete", model);
            }
        }

        private void ValidateTagProjectUserPermissionModel(TagProjectUserPermissionModel model, out User user)
        {
            if (string.IsNullOrWhiteSpace(model.UserName)) throw new Exception("Введите имя пользователя.");

            user = _userService.GetByName(model.UserName);
            if (user == null) throw new Exception("Пользователь не найден.");

            if (!model.SelectedPermissions.Any()) throw new Exception("Укажите полномочия.");
        }
    }
}