using CQRSlite.Config;
using CQRSLiteDemo.Domain.CommandHandlers;
using CQRSLiteDemo.Web.Commands.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CQRSLiteDemo.Web.Commands
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            StructuremapWebApi.Start();

            RegisterHandlers((IServiceLocator)GlobalConfiguration.Configuration.DependencyResolver);
        }

        private void RegisterHandlers(IServiceLocator serviceLocator)
        {
            var registrar = new BusRegistrar(serviceLocator);

            //By calling the line below, CQRSlite will register *all* Command Handlers in our project.  We don't need to explicitly register any additional ones. - MPJ 8/2/2016
            registrar.Register(typeof(EmployeeCommandHandler));
        }
    }
}
