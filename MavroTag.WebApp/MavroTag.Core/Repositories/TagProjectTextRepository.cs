using MavroTag.Core.Data;
using MavroTag.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MavroTag.Core.Repositories
{
	public class TagProjectTextRepository : BaseRepository<Domain.TagProjectText>, ITagProjectTextRepository
	{
		public TagProjectTextRepository(IMavroTagDbContext db) : base(db)
		{
		}
	}
}
