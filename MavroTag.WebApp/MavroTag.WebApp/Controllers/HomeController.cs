using MavroTag.Core.Data;
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

        private void ValidateLogin(LoginModel model)
        {
            if (string.IsNullOrEmpty(model.Name)) throw new Exception("Введите имя.");
            if (string.IsNullOrEmpty(model.Passphrase)) throw new Exception("Введите пароль.");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl = "")
        {
            try
            {
                ValidateLogin(model);

                var user = _userService.Login(model.Name, model.Passphrase);
                if (user == null) throw new Exception("Доступ закрыт.");

                AuthHelper.Login(user);

                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Office", "Home");
                }
            }
            catch (Exception e)
            {
                _logger.Error("Login", e);
                return View("Login", new LoginModel() { Error = e.Message });
            }
        }

        public ActionResult Logout()
        {
            AuthHelper.Logout();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Office()
        {
            return View();
        }
    }
}