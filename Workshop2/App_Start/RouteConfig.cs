using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Workshop2
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}/{name}/{category}/{status}/{member}",
                defaults: new { controller = "Books", action = "Index", id = UrlParameter.Optional,
                    name = UrlParameter.Optional,
                    category = UrlParameter.Optional,
                    status = UrlParameter.Optional,
                    member = UrlParameter.Optional }
            );
        }
    }
}
