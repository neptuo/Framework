using Neptuo.Web.Routing;
using Neptuo.Web.Services;
using Neptuo.Web.Services.Behaviors;
using Neptuo.Web.Services.Hosting;
using Neptuo.Web.Services.Hosting.Behaviors;
using Neptuo.Web.Services.Hosting.Behaviors.Providers;
using Neptuo.Web.Services.Hosting.Pipelines;
using Neptuo.Web.Services.Hosting.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TestMvc4.Controllers;
using TestMvc4.Handlers;
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


            ServiceEnvironment.Behaviors.Add(new InterfaceBehaviorProvider(typeof(IGet), typeof(GetBehavior)));
            ServiceEnvironment.Behaviors.Add(new InterfaceBehaviorProvider(typeof(IWithOutput<>), typeof(WithOutputBehavior<>)));

            ServiceEnvironment.RouteTable.Add(
                new UriRoute("~/hello.html", UriKind.Relative), 
                new CodeDomPipelineFactory(typeof(GetHello), ServiceEnvironment.Behaviors, new CodeDomPipelineConfiguration(@"C:\Temp", HttpRuntime.BinDirectory))
            );
        }
    }
}