using MavroTag.Core.Domain;
using MavroTag.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MavroTag.WebApp.Models
{
    public class TagProjectTextsModel : BaseModel
    {
        public List<TagProjectTextModel> TagProjectTexts { get; set; }
        public long TagProjectId { get; set; }
        public string TagProjectName { get; set; }
        public long TagProjectCategoryId { get; set; }
        public string TagProjectCategoryName { get; set; }
    }
}