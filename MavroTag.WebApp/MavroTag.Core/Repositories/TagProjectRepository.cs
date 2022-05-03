using MavroTag.Core.Data;
using MavroTag.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MavroTag.Core.Repositories
{
	public class TagProjectRepository : BaseRepository<Domain.TagProject>, ITagProjectRepository
	{
		public TagProjectRepository(IMavroTagDbContext db) : base(db)
		{
		}
	}
}
