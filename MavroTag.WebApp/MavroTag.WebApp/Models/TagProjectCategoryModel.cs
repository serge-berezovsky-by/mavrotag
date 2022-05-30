using AutoMapper;
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
        public string Description { get; set; }
        public DateTime AddedDateTime { get; set; }
        public long TextCount { get; set; }

        public static TagProjectCategoryModel FromTagProjectCategory(TagProjectCategory tagProjectCategory)
        {
            var tagProjectCategoryModel = Mapper.Map<TagProjectCategory, TagProjectCategoryModel>(tagProjectCategory);
            return tagProjectCategoryModel;
        }

        public static TagProjectCategory ToTagProjectCategory(TagProjectCategoryModel model, TagProjectCategory tagProjectCategory)
        {
            tagProjectCategory = tagProjectCategory != null ? Mapper.Map<TagProjectCategoryModel, TagProjectCategory>(model, tagProjectCategory) : Mapper.Map<TagProjectCategoryModel, TagProjectCategory>(model);
            return tagProjectCategory;
        }
    }
}