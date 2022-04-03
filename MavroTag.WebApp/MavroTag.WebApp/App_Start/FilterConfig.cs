using MavroTag.WebApp.Filters;
using System.Web;
using System.Web.Mvc;

namespace MavroTag.WebApp
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new LogHandleErrorAttribute());
        }
    }
}
