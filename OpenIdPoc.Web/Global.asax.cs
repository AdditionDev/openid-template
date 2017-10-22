using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using OpenIdPoc.Web.Start;

namespace OpenIdPoc.Web
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RegisterRoutes(RouteTable.Routes);
        }

        private static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Default", "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = "" });
        }
    }
}