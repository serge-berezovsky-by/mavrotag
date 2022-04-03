using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace MavroTag.WebApp.Filters
{
	public class LogHandleErrorAttribute : HandleErrorAttribute
	{
		protected static ILog _logger = LogManager.GetLogger(MethodInfo.GetCurrentMethod().DeclaringType);

		public override void OnException(ExceptionContext filterContext)
		{
			_logger.Error($"OnException {JsonConvert.SerializeObject(filterContext.RequestContext.RouteData.Values)}", filterContext.Exception);

			throw new Exception("OnException", filterContext.Exception);
		}
	}
}