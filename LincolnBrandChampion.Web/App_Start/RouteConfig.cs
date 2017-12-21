using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LincolnBrandChampion.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapMvcAttributeRoutes();

            routes.MapRoute(
           name: "Guest",
            url: "Guest",
            defaults: new { controller = "Guest", action = "Index" }
           );
            routes.MapRoute(
             name: "Registration",
             url: "Registration",
              defaults: new { controller = "Registration", action = "Index" }
              );

            routes.MapRoute(
            name: "BrandChampion",
            url: "BrandChampion",
            defaults: new { controller = "BrandChampion", action = "MakeConnections" }
            );
            routes.MapRoute(
           name: "Resources",
           url: "Resources",
           defaults: new { controller = "Resources", action = "Index" }
           );
            routes.MapRoute(
          name: "Toolkit",
          url: "Toolkit",
          defaults: new { controller = "Toolkit", action = "Index" }
          );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "LBC", action = "Welcome", id = UrlParameter.Optional }
            );
                    
            

        }
    }
}
