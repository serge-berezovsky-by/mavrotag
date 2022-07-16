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

        public ActionResult Add(long tagProjectCategoryId)
        {
            CheckPermissions(tagProjectCategoryId, out var tagProjectCategory, out var tagProject);

            var tagProjectText = new TagProjectText()
            {
                Id = 0,
                TagProjectCategoryId = tagProjectCategoryId,
                Name = string.Empty,
                Description = string.Empty,
                Content = string.Empty
            };

            var model = TagProjectTextModel.FromTagProjectText(tagProjectText);

            return View("Edit", model);
        }

        public ActionResult Edit(long id)
        {
            var tagProjectText = _tagProjectTextService.GetById(id);
            CheckPermissions(tagProjectText.TagProjectCategoryId, out var tagProjectCategory, out var tagProject);

            var model = TagProjectTextModel.FromTagProjectText(tagProjectText);

            return View("Edit", model);
        }

        public ActionResult Save(TagProjectTextModel model)
        {
            CheckPermissions(model.TagProjectCategoryId, out var tagProjectCategory, out var tagProject);

            try
            {
                ValidateTagProjectTextModel(model);
                ModelState.Clear();

                if (model.Id == 0)
                {
                    var tagProjectText = TagProjectTextModel.ToTagProjectText(model, null);
                    tagProjectText.AddedDateTime = DateTime.Now.ToUniversalTime();
                    tagProjectText = _tagProjectTextService.Add(tagProjectText);
                    model = TagProjectTextModel.FromTagProjectText(tagProjectText);
                    model.Success = "Текст добавлен.";
                }
                else
                {
                    var tagProjectText = _tagProjectTextService.GetById(model.Id);
                    tagProjectText = TagProjectTextModel.ToTagProjectText(model, tagProjectText);
                    _tagProjectTextService.Update(tagProjectText);
                    tagProjectText = _tagProjectTextService.GetById(model.Id);
                    model = TagProjectTextModel.FromTagProjectText(tagProjectText);
                    model.Success = "Текст сохранён.";
                }

                return View("Edit", model);
            }
            catch (Exception e)
            {
                _logger.Error("Save", e);
                model.Error = e.Message;
                return View("Edit", model);
            }
        }

        public ActionResult Delete(int id)
        {
            var tagProjectText = _tagProjectTextService.GetById(id);
            CheckPermissions(tagProjectText.TagProjectCategoryId, out var tagProjectCategory, out var tagProject);

            var model = new TagProjectTextModel();

            try
            {
                model = TagProjectTextModel.FromTagProjectText(tagProjectText);

                _tagProjectTextService.Delete(id);

                model.Success = $"Текст {model.Name} удалён.";

                return View("Delete", model);
            }
            catch (Exception e)
            {
                _logger.Error("Delete", e);
                model.Error = e.Message;
                return View("Delete", model);
            }
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

        private void ValidateTagProjectTextModel(TagProjectTextModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Name)) throw new Exception("Введите имя.");
            if (string.IsNullOrWhiteSpace(model.Content)) throw new Exception("Введите текст.");
        }
    }
}