using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace USP
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            //routes.IgnoreRoute("Static/*/*");
            //routes.IgnoreRoute("font/*");
            //routes.IgnoreRoute("Css/*");
            //routes.IgnoreRoute("Js/*");
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
