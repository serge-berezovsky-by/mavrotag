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
        void Update(TagProject tagProject);
        TagProject Add(TagProject tagProject);
        TagProject GetById(long id);
        void Delete(long id);
    }
}
