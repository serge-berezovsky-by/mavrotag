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
	}
}
