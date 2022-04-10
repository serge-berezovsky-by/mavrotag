using AutoMapper;

using MavroTag.WebApp.Filters;
using MavroTag.WebApp.Mapping;
using System.Web;
using System.Web.Mvc;

namespace MavroTag.WebApp
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(c => c.AddProfile(new MappingProfile()));
        }
    }
}
