using log4net;
using log4net.Config;
using MediaConverter.FFMPEGProvider;
using MediaConverter.FFMPEGProvider.Interfaces;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using System.Configuration;
using System.Web.Http;

namespace MediaConverter.Service
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private static readonly ILog _log = LogManager.GetLogger("MediaConverter.Service.Logger");
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            XmlConfigurator.Configure();

            string workFolder = ConfigurationManager.AppSettings["WorkFolder"];

            Container container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            container.Register<IMediaProvider>(() => new MediaProvider(workFolder, _log), Lifestyle.Singleton);
            container.Register<ILog>(() => _log, Lifestyle.Singleton);
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);

            _log.Info("Service started");
        }
    }
}
