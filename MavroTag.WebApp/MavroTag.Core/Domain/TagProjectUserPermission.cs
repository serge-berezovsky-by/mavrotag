using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MavroTag.Core.Domain
{
    public class TagProjectUserPermission : BaseEntity
    {
        public DateTime AddedDateTime { get; set; }
        public long TagProjectId { get; set; }
        public TagProject TagProject { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
        public string Permission { get; set; }
    }
}
