using MavroTag.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MavroTag.Core.Repositories
{
	public class PermissionRepository : BaseRepository<Domain.Permission>, IPermissionRepository
	{
		public PermissionRepository(IMavroTagDbContext db) : base(db)
		{
		}
	}
}
