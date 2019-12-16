using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using FizikaWeb.Models;

namespace FizikaWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            SQLConfig.SQLConnectionString = @"Provider = Microsoft.ACE.OLEDB.12.0;
            Data Source = C:\Users\hyptexxx3\Documents\visual studio 2017\Projects\FizikaWeb\fizika.accdb;
            Persist Security Info = False; ";
        }
    }
}
