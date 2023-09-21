
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
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

        protected void Application_Start()
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