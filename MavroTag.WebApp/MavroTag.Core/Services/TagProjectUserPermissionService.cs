using MavroTag.Core.Domain;
using MavroTag.Core.Interfaces;
using MavroTag.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MavroTag.Core.Services
{
    public class TagProjectUserPermissionService : ITagProjectUserPermissionService
    {
        private readonly ITagProjectUserPermissionRepository _tagProjectUserPermissionRepository;

        public TagProjectUserPermissionService(ITagProjectUserPermissionRepository tagProjectUserPermissionRepository)
        {
            _tagProjectUserPermissionRepository = tagProjectUserPermissionRepository;
        }

        public IEnumerable<TagProjectUserPermission> GetAll()
        {
            return _tagProjectUserPermissionRepository.GetAll().Include(c => c.User).AsEnumerable();
        }

        public void Update(long tagProjectId, long userId, IEnumerable<string> permissions)
        {
            var tagProjectUserPermissions = _tagProjectUserPermissionRepository.GetAll().Where(c => c.TagProjectId == tagProjectId && c.UserId == userId);
            foreach(var tagProjectUserPermission in tagProjectUserPermissions.ToList())
            {
                _tagProjectUserPermissionRepository.Delete(tagProjectUserPermission.Id);
            }
            foreach(var permission in permissions.ToList())
            {
                var tagProjectUserPermission = new TagProjectUserPermission()
                {
                    AddedDateTime = DateTime.Now,
                    TagProjectId = tagProjectId,
                    UserId = userId,
                    Permission = permission
                };
                _tagProjectUserPermissionRepository.Insert(tagProjectUserPermission);
            }
        }
    }
}
