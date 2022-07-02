using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MavroTag.Core.Domain
{
    public class TagProjectText : BaseEntity
    {
        public long TagProjectCategoryId { get; set; }
        public TagProjectCategory TagProjectCategory { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime AddedDateTime { get; set; }
        public string Content { get; set; }
    }
}
