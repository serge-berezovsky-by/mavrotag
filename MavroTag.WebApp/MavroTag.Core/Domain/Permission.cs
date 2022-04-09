using MavroTag.Core.Domain;
using MavroTag.Core.Enums;
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

        public static Permission FromEnum(Permissions permissionValue)
        {
            return new Permission()
            {
                Name = $"{permissionValue}"
            };
        }

        public bool Is(Permission permission)
        {
            return this.Name == permission.Name;
        }
    }
}
