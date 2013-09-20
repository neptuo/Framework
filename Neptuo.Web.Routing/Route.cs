using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;

namespace Neptuo.Web.Routing
{
    public class Route : RouteBase
    {
        public string Url { get; set; }
        public IRouteHandler RouteHandler { get; set; }

        public Route(string url, IRouteHandler routeHandler)
        {
            if (url == null)
                throw new ArgumentNullException("url");

            if (routeHandler == null)
                throw new ArgumentNullException("routeHandler");

            Url = url;
            RouteHandler = routeHandler;
        }

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            throw new NotImplementedException();
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            throw new NotImplementedException();
        }
    }
}
