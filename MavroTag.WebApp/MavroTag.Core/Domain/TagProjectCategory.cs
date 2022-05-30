using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MavroTag.Core.Domain
{
    public class TagProjectCategory : BaseEntity
    {
        public long TagProjectId { get; set; }
        public TagProject TagProject { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime AddedDateTime { get; set; }
    }
}
