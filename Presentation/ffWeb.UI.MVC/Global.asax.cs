
using log4net;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System;
using System.Security.Claims;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ffWeb.UI.MVC
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        static ILog log;
        static LogWriter ent_logger;

        public MvcApplication()
        {
            var ensure_sqlserver_dll_is_copied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;

            DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(), false);

            IConfigurationSource config = ConfigurationSourceFactory.Create();
            ExceptionPolicyFactory factory = new ExceptionPolicyFactory(config);
            Logger.SetLogWriter(new LogWriterFactory(config).Create(), false);
            //ExceptionManager exManager = factory.CreateManager();
            //ExceptionPolicy.SetExceptionManager(factory.CreateManager(), false);

            log4net.Config.BasicConfigurator.Configure();
            log = log4net.LogManager.GetLogger(typeof(MvcApplication));

            ent_logger = new LogWriterFactory().Create();
            Logger.SetLogWriter(ent_logger, false);
        }

        protected void Application_Start()
        {
            try
            {
                AreaRegistration.RegisterAllAreas();

                WebApiConfig.Register(GlobalConfiguration.Configuration);
                FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
                RouteConfig.RegisterRoutes(RouteTable.Routes);
                BundleConfig.RegisterBundles(BundleTable.Bundles);
                AuthConfig.RegisterAuth();

                DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(), false);

                // Code that runs on application startup
                Application["OnlineUsers"] = 0;

                AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.Name;

            }
            catch (Exception ex)
            {
                ent_logger.Write(ex.ToString());
                log.Info(ex.ToString());
            }
        }
        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started
            Application.Lock();
            Application["OnlineUsers"] = (int)Application["OnlineUsers"] + 1;
            Application.UnLock();
        }
        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.
            Application.Lock();
            Application["OnlineUsers"] = (int)Application["OnlineUsers"] - 1;
            Application.UnLock();
        }




    }
}