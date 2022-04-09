﻿using MavroTag.Core.Data;
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
                Users = users.Select(c => new UserModel() { User = c }).ToList()
            };
            return View(model);
        }

    }
}