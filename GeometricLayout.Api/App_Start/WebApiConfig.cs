using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace GeometricLayout.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
           

            //route.DataTokens["UseNamespaceFallback"] = false;
            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "{controller}/{action}",
            //    defaults: new { controller = "plotter", action="triangle" }
            //);
            config.MapHttpAttributeRoutes();
        }
    }
}
