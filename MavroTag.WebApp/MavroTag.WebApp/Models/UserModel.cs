using AutoMapper;
using MavroTag.Core.Domain;
using MavroTag.Core.Enums;
using MavroTag.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MavroTag.WebApp.Models
{
    public class UserModel : BaseModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Passphrase { get; set; }
        public Dictionary<string, bool> Permissions { get; set; }
        public Dictionary<string, string> PermissionNames { get; set; }

        private void SetPermissions(IEnumerable<Permission> permissions)
        {
            Permissions = new Dictionary<string, bool>();
            foreach (var permissionValue in Enum.GetValues(typeof(Permissions)).Cast<Permissions>())
            {
                var permission = Permission.FromEnum(permissionValue);
                Permissions[permission.Name] = permissions.Any(c => c.Is(permission));
            };
            PermissionNames = Permissions.ToDictionary(c => c.Key, c => c.Key);
        }

        public static UserModel FromUser(User user)
        {
            var userModel = Mapper.Map<User, UserModel>(user);
            userModel.SetPermissions(user.Permissions);
            return userModel;
        }

        public static User ToUser(UserModel model)
        {
            var user = Mapper.Map<UserModel, User>(model);
            user.Permissions = DependencyResolver.Current.GetService<IPermissionService>().GetAll()
                .Where(c => model.Permissions.Where(d => d.Value).Any(d => c.Is(Permission.FromString(d.Key))))
                .ToList();
            return user;
        }
    }
}