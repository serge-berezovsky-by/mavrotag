using MavroTag.Core.Domain;
using MavroTag.Core.Enums;
using MavroTag.Core.Helper;
using MavroTag.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MavroTag.Core.Data
{
    public class DatabaseHelper
    {
        private IUserService _userService;
        private IPermissionService _permissionService;

        public DatabaseHelper(IUserService userService, IPermissionService permissionService)
        {
            _userService = userService;
            _permissionService = permissionService;
        }

        public void AddAdministratorPermissions()
        {
            var permissions = _permissionService.GetAll().ToList();

            foreach (var permissionValue in Enum.GetValues(typeof(Permissions)).Cast<Permissions>())
            {
                var newPermission = new Permission()
                {
                    Name = $"{permissionValue}"
                };
                if (!permissions.Select(c => c.Name).Contains(newPermission.Name))
                {
                    _permissionService.Add(newPermission);
                }
            };


            var user = _userService.GetByName(Strings.AdministratorName);

            user.Permissions.Clear();

            foreach (var permission in _permissionService.GetAll())
            {
                user.Permissions.Add(permission);
            }

            _userService.Update(user);
        }
    }
}
