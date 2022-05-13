using MavroTag.Core.Domain;
using MavroTag.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MavroTag.WebApp.Models
{
    public class TagProjectCategoryModel : BaseModel
    {
        public long Id { get; set; }
        public long TagProjectId { get; set; }
        public string Name { get; set; }
        public long TextCount { get; set; }
    }
}