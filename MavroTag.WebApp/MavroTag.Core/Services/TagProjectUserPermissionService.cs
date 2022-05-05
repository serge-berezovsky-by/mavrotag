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

        public TagProjectUserPermission GetById(long id)
        {
            var tagProjectUserPermission = _tagProjectUserPermissionRepository.GetAll().FirstOrDefault(c => c.Id == id);
            return tagProjectUserPermission;
        }

        public TagProjectUserPermission Add(TagProjectUserPermission tagProjectUserPermission)
        {
            return _tagProjectUserPermissionRepository.Insert(tagProjectUserPermission);
        }

        public void Delete(long id)
        {
            _tagProjectUserPermissionRepository.Delete(id);
        }
    }
}
