using MavroTag.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MavroTag.Core.Interfaces
{
    public interface ITagProjectService
    {
        IEnumerable<TagProject> GetAll();
        void Update(TagProject user);
        TagProject Add(TagProject user);
        TagProject GetById(long id);
        void Delete(long id);
    }
}
