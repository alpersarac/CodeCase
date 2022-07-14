using ConfigLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CodeCase
{
    public class MvcApplication : System.Web.HttpApplication
    {

        TimedHostedService timedHostedService;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            timedHostedService = new TimedHostedService();


            string ConnectionString = ConfigHelper.SetConnectionString("DESKTOP-QCR22I6", "AllYouPlay");
            timedHostedService.ConfigurationReader("SERVICE-A", ConnectionString, 8000);
        }
    }
}
