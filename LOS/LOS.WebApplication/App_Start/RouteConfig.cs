﻿using System.Web.Mvc;
using System.Web.Routing;

namespace LOS.WebApplication.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.LowercaseUrls = true;

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}/{page}/{keyword}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional, page = UrlParameter.Optional, keyword = "" },
				namespaces: new[] { "LOS.WebApplication.Controllers" }
            );
        }
    }
}
