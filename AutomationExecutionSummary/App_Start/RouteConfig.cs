using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AutomationExecutionSummary
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{buildRunId}/{suiteName}",
                defaults: new { controller = "Dashboard", action = "Index", buildRunId = UrlParameter.Optional, suiteName = UrlParameter.Optional }
            );
        }
    }
}