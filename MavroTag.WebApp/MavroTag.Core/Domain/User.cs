using MavroTag.Core.Domain;
using MavroTag.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MavroTag.Core.Domain
{
	public class User : BaseEntity
	{
		public string Name { get; set; }
		public string Passphrase { get; set; }
		public ICollection<Permission> Permissions { get; set; }

		public User()
		{
            Permissions = new HashSet<Permission>();
		}

        public bool HasPermission(Permissions permissionValue)
        {
			var permission = Permission.FromEnum(permissionValue);
			return Permissions.Any(c => c.Is(permission));
        }
    }
}
