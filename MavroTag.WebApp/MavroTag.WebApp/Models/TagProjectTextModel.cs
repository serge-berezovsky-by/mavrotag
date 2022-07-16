using AutoMapper;
using MavroTag.Core.Domain;
using MavroTag.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MavroTag.WebApp.Models
{
    public class TagProjectTextModel : BaseModel
    {
        public long Id { get; set; }
        public long TagProjectCategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime AddedDateTime { get; set; }
        [AllowHtml]
        public string Content { get; set; }

        public static TagProjectTextModel FromTagProjectText(TagProjectText tagProjectText)
        {
            var tagProjectTextModel = Mapper.Map<TagProjectText, TagProjectTextModel>(tagProjectText);
            return tagProjectTextModel;
        }

        public static TagProjectText ToTagProjectText(TagProjectTextModel model, TagProjectText tagProjectText)
        {
            tagProjectText = tagProjectText != null ? Mapper.Map<TagProjectTextModel, TagProjectText>(model, tagProjectText) : Mapper.Map<TagProjectTextModel, TagProjectText>(model);
            return tagProjectText;
        }
    }
}