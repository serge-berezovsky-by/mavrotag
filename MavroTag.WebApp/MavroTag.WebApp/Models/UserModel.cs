using MavroTag.Core.Domain;
using System.Collections.Generic;

namespace MavroTag.WebApp.Models
{
    public class UserModel : BaseModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Passphrase { get; set; }
        public List<Permission> Permissions { get; set; }
    }
}