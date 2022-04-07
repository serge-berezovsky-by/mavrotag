using MavroTag.Core.Data;
using MavroTag.Core.Interfaces;
using MavroTag.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MavroTag.WebApp.Controllers
{
    public class HomeController : BaseController
    {
        private IUserService _userService;
        private IPermissionService _permissionService;

        public HomeController(IUserService userService, IPermissionService permissionService)
        {
            _userService = userService;
            _permissionService = permissionService;
        }

        public ActionResult Index()
        {
            var users = _userService.GetAll().ToList();
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }

        public ActionResult NotFound()
        {
            return View();
        }

        public ActionResult Update()
        {
            var databaseHelper = new DatabaseHelper(_userService, _permissionService);
            databaseHelper.AddAdministratorPermissions();
            return View();
        }
        public ActionResult Login(string returnUrl)
        {
            return View(new LoginModel());
        }
    }
}