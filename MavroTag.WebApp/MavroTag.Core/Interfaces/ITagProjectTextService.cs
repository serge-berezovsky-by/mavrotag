using MavroTag.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MavroTag.Core.Interfaces
{
    public interface ITagProjectTextService
    {
        IEnumerable<TagProjectText> GetAll();
        IEnumerable<TagProjectText> GetByCategoryId(long tagProjectCategoryId);
        void Update(TagProjectText tagProjectText);
        TagProjectText Add(TagProjectText tagProjectText);
        TagProjectText GetById(long id);
        void Delete(long id);
    }
}
