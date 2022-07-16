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
        ITagProjectCategoryService _tagProjectCategoryService;
        ITagProjectTextService _tagProjectTextService;

        public TagProjectCategoriesController(ITagProjectService tagProjectService,
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

        public ActionResult Index(long tagProjectId)
        {
            CheckPermissions(tagProjectId, out var tagProject);

            var model = new TagProjectCategoriesModel()
            {
                TagProjectId = tagProjectId,
                TagProjectName = tagProject.Name,
                TagProjectCategories = new List<TagProjectCategoryModel>()
            };

            model.TagProjectCategories = _tagProjectCategoryService.GetAll().Select(c => TagProjectCategoryModel.FromTagProjectCategory(c)).ToList();

            foreach(var tagProjectCategory in model.TagProjectCategories)
            {
                tagProjectCategory.TextCount = _tagProjectTextService.GetByCategoryId(tagProjectCategory.Id).Count();
            };

            return View(model);
        }

        public ActionResult Add(long tagProjectId)
        {
            CheckPermissions(tagProjectId, out var tagProject);

            var tagProjectCategory = new TagProjectCategory()
            {
                Id = 0,
                Name = string.Empty,
                Description = string.Empty,
                TagProjectId = tagProjectId
            };

            var model = TagProjectCategoryModel.FromTagProjectCategory(tagProjectCategory);

            return View("Edit", model);
        }

        public ActionResult Edit(long id)
        {
            var tagProjectCategory = _tagProjectCategoryService.GetById(id);
            CheckPermissions(tagProjectCategory.TagProjectId, out var tagProject);

            var model = TagProjectCategoryModel.FromTagProjectCategory(tagProjectCategory);

            return View("Edit", model);
        }

        public ActionResult Save(TagProjectCategoryModel model)
        {
            CheckPermissions(model.TagProjectId, out var tagProject);

            try
            {
                ValidateTagProjectCategoryModel(model);
                ModelState.Clear();

                if (model.Id == 0)
                {
                    var tagProjectCategory = TagProjectCategoryModel.ToTagProjectCategory(model, null);
                    tagProjectCategory.AddedDateTime = DateTime.Now.ToUniversalTime();
                    tagProjectCategory = _tagProjectCategoryService.Add(tagProjectCategory);
                    model = TagProjectCategoryModel.FromTagProjectCategory(tagProjectCategory);
                    model.Success = "Категория добавлена.";
                }
                else
                {
                    var tagProjectCategory = _tagProjectCategoryService.GetById(model.Id);
                    tagProjectCategory = TagProjectCategoryModel.ToTagProjectCategory(model, tagProjectCategory);
                    _tagProjectCategoryService.Update(tagProjectCategory);
                    tagProjectCategory = _tagProjectCategoryService.GetById(model.Id);
                    model = TagProjectCategoryModel.FromTagProjectCategory(tagProjectCategory);
                    model.Success = "Категория сохранена.";
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
            var tagProjectCategory = _tagProjectCategoryService.GetById(id);
            CheckPermissions(tagProjectCategory.TagProjectId, out var tagProject);

            var model = new TagProjectCategoryModel();

            try
            {
                model = TagProjectCategoryModel.FromTagProjectCategory(tagProjectCategory);

                _tagProjectCategoryService.Delete(id);

                model.Success = $"Категория {model.Name} удалена.";

                return View("Delete", model);
            }
            catch (Exception e)
            {
                _logger.Error("Delete", e);
                model.Error = e.Message;
                return View("Delete", model);
            }
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

        private void ValidateTagProjectCategoryModel(TagProjectCategoryModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Name)) throw new Exception("Введите имя.");
        }
    }
}