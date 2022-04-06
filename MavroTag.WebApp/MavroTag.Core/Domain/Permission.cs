using MavroTag.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MavroTag.Core.Domain
{
	public class Permission : BaseEntity
	{
		public string Name { get; set; }
		public ICollection<User> Users { get; set; }

		public Permission()
		{
			Users = new HashSet<User>();
		}
	}
}
