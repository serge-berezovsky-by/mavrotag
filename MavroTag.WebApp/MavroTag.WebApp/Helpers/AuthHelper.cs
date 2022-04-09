﻿using MavroTag.Core.Domain;
using MavroTag.Core.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MavroTag.WebApp.Helpers
{
    public static class AuthHelper
    {
        private static ConcurrentDictionary<string, User> _users = new ConcurrentDictionary<string, User>();

        public static bool IsAuthenticated => HttpContext.Current?.User?.Identity?.IsAuthenticated == true;

        public static string Name => GetUser()?.Name;

        public static void Login(User user)
        {
            FormsAuthentication.SetAuthCookie(user.Name, false);
            _users.TryRemove(user.Name, out _);
        }

        private static User GetUser()
        {
            var name = HttpContext.Current.User.Identity.Name;
            if (!_users.ContainsKey(name))
            {
                var userService = DependencyResolver.Current.GetService<IUserService>();
                var user = userService.GetByName(name);
                _users.TryAdd(name, user);
            }
            return _users[name];
        }

        public static void Logout()
        {
            FormsAuthentication.SignOut();
        }
    }
}