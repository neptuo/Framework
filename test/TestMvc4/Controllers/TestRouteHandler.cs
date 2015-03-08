using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace TestMvc4.Controllers
{
    public class TestRouteHandler : IRouteHandler
    {
        private bool notFound;

        public TestRouteHandler(bool notFound = false)
        {
            this.notFound = notFound;
        }

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new TestHandler(notFound, requestContext.RouteData);
        }
    }
}