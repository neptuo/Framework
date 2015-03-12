using Neptuo.Web.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TestMvc4.Controllers;
using TestMvc4.Models;

namespace TestMvc4
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            //RouteParameters.Registry.Add("locale", new LocaleRouteParameter("cs-cz"));



            //routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.Add(new TokenRoute("~/text", new TestRouteHandler()));
            //routes.Add(new TokenRoute("~/HelloWorld", new TestRouteHandler(), ".aspx"));
            //routes.Add(new TokenRoute("~/{Locale}/HelloWorld", new TestRouteHandler()));
            //routes.Add(new Route("{*pathInfo}", new TestRouteHandler(true)));

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}