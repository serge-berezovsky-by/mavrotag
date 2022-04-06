using MavroTag.Core.Domain;
using MavroTag.Core.Interfaces;
using MavroTag.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MavroTag.Core.Services
{
	public class PermissionService : IPermissionService
	{
		private readonly IPermissionRepository _permissionRepository;

		public PermissionService(IPermissionRepository permissionRepositoryy)
		{
			_permissionRepository = permissionRepositoryy;
		}

		public IEnumerable<Permission> GetAll()
		{
			return _permissionRepository.GetAll().AsEnumerable();
		}

		public Permission GetById(long id)
		{
			return _permissionRepository.GetById(id);
		}

		public Permission Add(Permission permission)
		{
			return _permissionRepository.Insert(permission);
		}
	}
}
