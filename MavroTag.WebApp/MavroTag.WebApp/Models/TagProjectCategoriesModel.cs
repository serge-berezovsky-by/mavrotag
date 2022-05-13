using MavroTag.Core.Domain;
using MavroTag.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MavroTag.WebApp.Models
{
    public class TagProjectCategoriesModel : BaseModel
    {
        public List<TagProjectCategoryModel> TagProjectCategories { get; set; }
        public long TagProjectId { get; set; }
        public string TagProjectName { get; set; }
    }
}