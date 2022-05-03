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
    public class TagProjectsController : BaseController
    {
        ITagProjectService _tagProjectService;

        public TagProjectsController(ITagProjectService tagProjectService)
        {
            _tagProjectService = tagProjectService;
        }

        public ActionResult Index()
        {
            AuthHelper.Check(Permissions.MyTagProjects);

            var tagProjects = _tagProjectService.GetAll().Where(c => c.UserId == AuthHelper.UserId).ToList();
            var model = new TagProjectsModel()
            {
                TagProjects = tagProjects.Select(c => TagProjectModel.FromTagProject(c)).ToList(),
            };

            return View(model);
        }

        public ActionResult Add()
        {
            AuthHelper.Check(Permissions.MyTagProjects);

            var tagProject = new TagProject()
            {
                Id = 0,
                Name = string.Empty,
                Description = string.Empty,
                UserId = AuthHelper.UserId
            };

            var model = TagProjectModel.FromTagProject(tagProject);

            return View("Edit", model);
        }

        public ActionResult Edit(int id)
        {
            AuthHelper.Check(Permissions.MyTagProjects);

            var tagProject = _tagProjectService.GetById(id);

            if (tagProject == null) AuthHelper.LogoutAndRedirect();
            if (tagProject.UserId != AuthHelper.UserId) AuthHelper.LogoutAndRedirect();

            var model = TagProjectModel.FromTagProject(tagProject);

            return View("Edit", model);
        }

        public ActionResult Save(TagProjectModel model)
        {
            AuthHelper.Check(Permissions.MyTagProjects);

            try
            {
                ValidateTagProjectModel(model);
                ModelState.Clear();

                if (model.Id == 0)
                {
                    var tagProject = TagProjectModel.ToTagProject(model, null);
                    tagProject.AddedDateTime = DateTime.Now.ToUniversalTime();
                    tagProject.UserId = AuthHelper.UserId;
                    tagProject = _tagProjectService.Add(tagProject);
                    model = TagProjectModel.FromTagProject(tagProject);
                    model.Success = "Tag-проект добавлен.";
                }
                else
                {
                    var tagProject = _tagProjectService.GetById(model.Id);
                    tagProject = TagProjectModel.ToTagProject(model, tagProject);
                    _tagProjectService.Update(tagProject);
                    tagProject = _tagProjectService.GetById(model.Id);
                    model = TagProjectModel.FromTagProject(tagProject);
                    model.Success = "Tag-проект сохранён.";
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
            AuthHelper.Check(Permissions.MyTagProjects);

            var model = new TagProjectModel();

            try
            {
                var tagProject = _tagProjectService.GetById(id);
                model = TagProjectModel.FromTagProject(tagProject);

                if (tagProject == null) AuthHelper.LogoutAndRedirect();
                if (tagProject.UserId != AuthHelper.UserId) AuthHelper.LogoutAndRedirect();

                _tagProjectService.Delete(id);

                model.Success = $"Tag-проект {model.Name} удалён";

                return View("Delete", model);
            }
            catch (Exception e)
            {
                _logger.Error("Delete", e);
                model.Error = e.Message;
                return View("Delete", model);
            }
        }

        private void ValidateTagProjectModel(TagProjectModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Name)) throw new Exception("Введите имя.");

            if (model.Id == 0) // Add
            {
            }
            else // Edit
            {
                var tagProject = _tagProjectService.GetById(model.Id);
                if (tagProject == null) AuthHelper.LogoutAndRedirect();
                if (tagProject.UserId != AuthHelper.UserId) AuthHelper.LogoutAndRedirect();
            }
        }
    }
}