using AutoMapper;
using MavroTag.Core.Domain;
using MavroTag.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MavroTag.WebApp.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserModel>()
                .ForMember(c => c.Permissions, c => c.Ignore());

            CreateMap<UserModel, User>()
                .ForMember(c => c.Permissions, c => c.Ignore())
                .ForMember(c => c.AddedDateTime, c => c.Ignore());

            CreateMap<TagProject, TagProjectModel>();

            CreateMap<TagProjectModel, TagProject>()
                .ForMember(c => c.UserId, c => c.Ignore())
                .ForMember(c => c.AddedDateTime, c => c.Ignore());

            CreateMap<TagProjectCategory , TagProjectCategoryModel>();

            CreateMap<TagProjectCategoryModel, TagProjectCategory>()
                .ForMember(c => c.AddedDateTime, c => c.Ignore());

            CreateMap<TagProjectTextModel, TagProjectText>()
                .ForMember(c => c.AddedDateTime, c => c.Ignore());
        }
    }
}