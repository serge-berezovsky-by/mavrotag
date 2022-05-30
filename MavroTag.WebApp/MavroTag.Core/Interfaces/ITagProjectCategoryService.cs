using MavroTag.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MavroTag.Core.Interfaces
{
    public interface ITagProjectCategoryService
    {
        IEnumerable<TagProjectCategory> GetAll();
        void Update(TagProjectCategory tagProjectCategory);
        TagProjectCategory Add(TagProjectCategory tagProjectCategory);
        TagProjectCategory GetById(long id);
        void Delete(long id);
    }
}
