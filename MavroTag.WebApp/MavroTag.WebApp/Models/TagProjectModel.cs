using AutoMapper;
using MavroTag.Core.Domain;
using System;

namespace MavroTag.WebApp.Models
{
    public class TagProjectModel : BaseModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime AddedDateTime { get; set; }
        public long UserId { get; set; }

        public static TagProjectModel FromTagProject(TagProject tagProject)
        {
            var tagProjectModel = Mapper.Map<TagProject, TagProjectModel>(tagProject);
            return tagProjectModel;
        }

        public static TagProject ToTagProject(TagProjectModel model, TagProject tagProject)
        {
            tagProject = tagProject != null ? Mapper.Map<TagProjectModel, TagProject>(model, tagProject) : Mapper.Map<TagProjectModel, TagProject>(model);
            return tagProject;
        }
    }
}