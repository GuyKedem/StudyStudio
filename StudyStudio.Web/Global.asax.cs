using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Web;
using Autofac.Integration.Web.Mvc;
using Spark.Web.Mvc;
using StudyStudio.Infrastructure;

namespace StudyStudio.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    // ReSharper disable InconsistentNaming
    public class MvcApplication : HttpApplication, IContainerProviderAccessor
    {
        private static IContainerProvider _containerProvider;

        protected void Application_Start()
        {
            ConfigureDI();
            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);
            ViewEngines.Engines.Add(new SparkViewFactory());
        }

        private void ConfigureDI()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterModule(new InfrastructureModule(true));
            _containerProvider = new ContainerProvider(builder.Build());
            ControllerBuilder.Current.SetControllerFactory(new AutofacControllerFactory(ContainerProvider));
        }

        private static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
                );
        }

        public IContainerProvider ContainerProvider
        {
            get { return _containerProvider; }
        }
    }
}