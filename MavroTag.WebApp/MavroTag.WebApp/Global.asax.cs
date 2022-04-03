using Autofac;
using Autofac.Integration.Mvc;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MavroTag.WebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static ILog _looger = LogManager.GetLogger(MethodInfo.GetCurrentMethod().DeclaringType);

        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure();
            _looger.Info("Logging configured.");

            InitializeAutofac();

            //Database.SetInitializer(new MavroPediaDbInitializer());

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void InitializeAutofac()
        {
            var builder = new ContainerBuilder();

            //builder.RegisterType(typeof(MavroPediaDbContext))
            //    .AsImplementedInterfaces();

            //builder.RegisterAssemblyTypes(typeof(UserRepository).Assembly)
            //    .Where(c => c.Name.EndsWith("Repository"))
            //    .AsImplementedInterfaces();

            //builder.RegisterAssemblyTypes(typeof(UserService).Assembly)
            //    .Where(c => c.Name.EndsWith("Service"))
            //    .AsImplementedInterfaces();

            // Register your MVC controllers. (MvcApplication is the name of
            // the class in Global.asax.)
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // OPTIONAL: Register model binders that require DI.
            builder.RegisterModelBinders(typeof(MvcApplication).Assembly);
            builder.RegisterModelBinderProvider();

            // OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());

            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
