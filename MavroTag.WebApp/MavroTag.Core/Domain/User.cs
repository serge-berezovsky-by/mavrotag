using MavroTag.Core.Domain;
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
	}
}
