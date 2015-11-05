using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MovieRental
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute("ReturnOrder",
                            "Orders/Delete/",
                             new { controller = "Order", action = "Delete" },
                             new[] { "MovieRental.Controllers" });

            routes.MapRoute("OrderMovie",
                "Orders/Create/",
                 new { controller = "Order", action = "Create" },
                 new[] { "MovieRental.Controllers" });
        }
    }
}
