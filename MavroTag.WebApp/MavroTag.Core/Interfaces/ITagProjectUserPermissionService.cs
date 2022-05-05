using MavroTag.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MavroTag.Core.Interfaces
{
    public interface ITagProjectUserPermissionService
    {
        IEnumerable<TagProjectUserPermission> GetAll();
        TagProjectUserPermission Add(TagProjectUserPermission tagProjectUserPermission);
        TagProjectUserPermission GetById(long id);
        void Delete(long id);
    }
}
