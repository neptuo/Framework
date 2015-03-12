using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace TestMvc4.Controllers
{
    public class TestHandler : IHttpHandler
    {
        private bool notFound;
        private RouteData routeData;

        public bool IsReusable
        {
            get { return true; }
        }

        public TestHandler(bool notFound, RouteData routeData)
        {
            this.notFound = notFound;
            this.routeData = routeData;
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            if (notFound)
            {
                context.Response.Write("404 - Not Found");
            }
            else
            {
                foreach (var item in routeData.Values)
                    context.Response.Write(String.Format("{0}: {1}{2}", item.Key, item.Value, Environment.NewLine));

                if (routeData.DataTokens.ContainsKey("TemplateID"))
                    context.Response.Write(String.Format("TemplateID: {0}{1}", routeData.DataTokens["TemplateID"], Environment.NewLine));

                context.Response.Write(context.Request.AppRelativeCurrentExecutionFilePath);
            }
        }
    }
}