using MavroTag.Core.Domain;
using MavroTag.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MavroTag.WebApp.Models
{
    public class TagProjectUserPermissionModel
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public Dictionary<string, bool> Permissions { get; set; }
        public Dictionary<string, string> PermissionNames { get; set; }

        public static TagProjectUserPermissionModel FromTagProjectUserPermissions(User user, IEnumerable<TagProjectUserPermission> tagProjectUserPermissions)
        {
            var tagProjectUserPermissionModel = new TagProjectUserPermissionModel()
            {
                UserId = user.Id,
                UserName = user.Name
            };
            tagProjectUserPermissionModel.SetPermissions(tagProjectUserPermissions);
            return tagProjectUserPermissionModel;
        }

        private void SetPermissions(IEnumerable<TagProjectUserPermission> tagProjectUserPermissions)
        {
            Permissions = new Dictionary<string, bool>();
            foreach (var tagProjectPermissionValue in Enum.GetValues(typeof(TagProjectPermissions)).Cast<TagProjectPermissions>())
            {
                var tagProjectPermissionStringValue = tagProjectPermissionValue.ToString();
                Permissions[tagProjectPermissionStringValue] = tagProjectUserPermissions.Any(c => c.Permission == tagProjectPermissionStringValue);
            };
            PermissionNames = Permissions.ToDictionary(c => c.Key, c => c.Key);
        }
    }
}