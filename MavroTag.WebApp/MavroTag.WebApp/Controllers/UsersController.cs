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
    public class UsersController : BaseController
    {
        private IUserService _userService;
        private IPermissionService _permissionService;

        public UsersController(IUserService userService, IPermissionService permissionService)
        {
            _userService = userService;
            _permissionService = permissionService;
        }

        public ActionResult Index()
        {
            AuthHelper.Check(Permissions.ViewUsers);

            var users = _userService.GetAll().ToList();
            var model = new UsersModel()
            {
                Users = users.Select(c => UserModel.FromUser(c)).ToList()
            };

            return View(model);
        }

        public ActionResult Add()
        {
            AuthHelper.Check(Permissions.AddUser);

            var user = new User()
            {
                Id = 0,
                Name = string.Empty,
                Passphrase = Guid.NewGuid().ToString().ToUpper(),
                Description = string.Empty,
                IsEnabled = true,
                Permissions = new List<Permission>()
            };

            var model = UserModel.FromUser(user);

            return View("Edit", model);
        }

        public ActionResult Edit(int id)
        {
            AuthHelper.Check(Permissions.EditUser);

            var user = _userService.GetById(id);

            if (user == null) AuthHelper.LogoutAndRedirect();

            var model = UserModel.FromUser(user);

            return View("Edit", model);
        }

        public ActionResult Save(UserModel model)
        {
            if (model.Id == 0) AuthHelper.Check(Permissions.AddUser); else AuthHelper.Check(Permissions.EditUser);

            try
            {
                ValidateUserModel(model);
                ModelState.Clear();

                if (model.Id == 0)
                {
                    var user = UserModel.ToUser(model, null);
                    user.AddedDateTime = DateTime.Now.ToUniversalTime();
                    user = _userService.Add(user);
                    model = UserModel.FromUser(user);
                    model.Success = "Пользователь добавлен.";
                }
                else
                {
                    var user = _userService.GetById(model.Id);
                    user = UserModel.ToUser(model, user);
                    _userService.Update(user);
                    user = _userService.GetById(model.Id);
                    model = UserModel.FromUser(user);
                    model.Success = "Пользователь сохранён.";
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
            AuthHelper.Check(Permissions.DeleteUser);

            UserModel model = null;

            try
            {
                var user = _userService.GetById(id);
                model = UserModel.FromUser(user);

                if (user.IsAdministrator) throw new Exception("Нельзя удалить администратора.");
                if (string.Compare(AuthHelper.Name, model.Name, ignoreCase: true) == 0) throw new Exception("Нельзя удалить самого себя.");

                _userService.Delete(id);

                model.Success = $"Пользователь {model.Name} удалён";

                return View("Delete", model);
            }
            catch (Exception e)
            {
                _logger.Error("Delete", e);
                model.Error = e.Message;
                return View("Delete", model);
            }
        }

        private void ValidateUserModel(UserModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Name)) throw new Exception("Введите имя.");
            if (string.IsNullOrWhiteSpace(model.Passphrase)) throw new Exception("Введите код.");

            if (model.Id == 0) // Add
            {
            }
            else // Edit
            {
                var user = _userService.GetById(model.Id);
                if (user == null) throw new Exception("Пользователь не найден.");
            }

            var users = _userService.GetAll();
            if (users.Any(c => string.Compare(c.Name, model.Name, ignoreCase: true) == 0 && c.Id != model.Id)) throw new Exception("Имя занято.");
        }
    }
}