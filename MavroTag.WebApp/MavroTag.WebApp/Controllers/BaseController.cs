using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace MavroPedia.WebApp.Controllers
{
    public class BaseController : Controller
    {
        protected static ILog _logger = LogManager.GetLogger(MethodInfo.GetCurrentMethod().DeclaringType);

        public BaseController()
        {
        }
    }
}