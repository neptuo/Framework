using Neptuo.Tokens;
using Neptuo.Web.Routing.Segments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;

namespace Neptuo.Web.Routing
{
    public class TokenRoute : TokenRouteBase
    {
        protected List<RouteSegment> Segments { get; set; }

        public string Url { get; private set; }

        protected override bool IsAbsolute
        {
            get { return !Url.StartsWith("~/"); }
        }

        public TokenRoute(string url, IRouteHandler routeHandler, string suffix = null, bool caseSensitive = false, IRouteParameterService parameterService = null)
            : base(routeHandler, suffix, caseSensitive, parameterService)
        {
            Ensure.NotNull(url, "url");
            Url = url;
            Segments = BuildRoute(Url);
        }

        protected override bool MatchUrl(string url, RouteValueDictionary values)
        {
            if (!String.IsNullOrEmpty(Suffix) && !url.EndsWith(Suffix))
                return false;

            RouteSegmentContext context = new RouteSegmentContext(url, values);
            foreach (RouteSegment segment in Segments)
            {
                if (!segment.MatchUrl(context))
                    return false;

                context = new RouteSegmentContext(context.RemainingUrl, values);
            }

            if (!MatchSuffix(context.RemainingUrl))
                return false;

            return true;
        }

        protected override string GetRequestUrl(HttpContextBase httpContext)
        {
            if (IsAbsolute)
                return httpContext.Request.Url.ToString();

            return base.GetRequestUrl(httpContext);
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            //throw new NotImplementedException();
            return null;
        }
    }
}
