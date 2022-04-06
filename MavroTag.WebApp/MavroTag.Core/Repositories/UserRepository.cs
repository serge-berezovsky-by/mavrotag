using MavroTag.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MavroTag.Core.Repositories
{
	public class UserRepository : BaseRepository<Domain.User>, IUserRepository
	{
		public UserRepository(IMavroTagDbContext db) : base(db)
		{
		}

		public override void Update(Domain.User user)
		{
			var permissionIds = user.Permissions.Select(c => c.Id).ToList();
			var permissions = _db.Set<Domain.Permission>();
			user.Permissions = permissions.Where(c => permissionIds.Contains(c.Id)).ToList();
			base.Update(user);
		}
	}
}
