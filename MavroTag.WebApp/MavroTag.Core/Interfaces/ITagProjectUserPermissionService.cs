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
        void Update(long tagProjectId, long userId, IEnumerable<string> permissions);
    }
}
