using AutoMapper;
using MavroTag.Core.Data;
using MavroTag.Core.Domain;
using MavroTag.Core.Enums;
using MavroTag.Core.Interfaces;
using MavroTag.WebApp.Helpers;
using MavroTag.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MavroTag.WebApp.Controllers
{
    public class TagProjectsController : BaseController
    {
        public TagProjectsController()
        {
        }

        public ActionResult Index()
        {
            AuthHelper.Check(Permissions.MyTagProjects);

            var model = new TagProjectsModel()
            {
                TagProjects = new List<TagProjectModel>()
            };

            return View(model);
        }
    }
}