using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MavroTag.Core.Domain
{
    public class TagProject : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime AddedDateTime { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
    }
}
