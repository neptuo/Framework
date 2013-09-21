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
    public class TokenRoute : RouteBase
    {
        protected List<RouteSegment> Segments { get; set; }
        protected TokenParser TokenParser { get; private set; }
        protected IRouteParameterService ParameterService { get; private set; }

        public string Url { get; set; }
        public IRouteHandler RouteHandler { get; set; }
        public string Suffix { get; set; }
        public bool CaseSensitive { get; set; }

        public TokenRoute(string url, IRouteHandler routeHandler, string suffix = null, bool caseSensitive = false, IRouteParameterService parameterService = null)
        {
            if (url == null)
                throw new ArgumentNullException("url");

            if (routeHandler == null)
                throw new ArgumentNullException("routeHandler");

            CaseSensitive = caseSensitive;
            if (!CaseSensitive)
                url = url.ToLowerInvariant();

            Url = url;
            RouteHandler = routeHandler;
            Suffix = suffix;
            ParameterService = parameterService ?? RouteParameters.Service;
            TokenParser = CreateTokenParser();
            Segments = BuildRoute(Url);
        }

        protected virtual TokenParser CreateTokenParser()
        {
            TokenParser parser = new TokenParser();
            parser.Configuration.AllowTextContent = true;
            return parser;
        }

        protected virtual List<RouteSegment> BuildRoute(string routeUrl)
        {
            List<RouteSegment> result = new List<RouteSegment>();

            int lastIndex = 0;
            TokenParser.OnParsedToken += (sender, e) =>
            {
                if (e.StartPosition > lastIndex)
                    result.Add(new StaticRouteSegment(routeUrl.Substring(lastIndex, e.StartPosition - lastIndex)));

                result.Add(new ParamRouteSegment(ParameterService.Get(e.Token.Fullname)));
                lastIndex = e.EndPosition + 1;
            };

            if (!TokenParser.Parse(routeUrl))
                throw new ArgumentOutOfRangeException("routeUrl", "Route url is not valid format for TokenRoute.");

            if (routeUrl.Length > lastIndex)
                result.Add(new StaticRouteSegment(routeUrl.Substring(lastIndex)));

            return result;
        }

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            string requestUrl = GetRequestUrl(httpContext);
            if (requestUrl == null)
                return null;

            if (!CaseSensitive)
                requestUrl = requestUrl.ToLowerInvariant();

            RouteData routeData = new RouteData(this, RouteHandler);
            if (MatchUrl(requestUrl, routeData.Values, Segments))
                return routeData;

            return null;
        }

        protected virtual string GetRequestUrl(HttpContextBase httpContext)
        {
            if (Url.StartsWith("~/"))
                return httpContext.Request.AppRelativeCurrentExecutionFilePath + httpContext.Request.PathInfo;

            return httpContext.Request.Url.ToString();
        }

        protected virtual bool MatchUrl(string url, RouteValueDictionary values, List<RouteSegment> route)
        {
            RouteSegmentContext context = new RouteSegmentContext(url, values);
            foreach (RouteSegment segment in route)
            {
                if (!segment.MatchUrl(context))
                    return false;

                context = new RouteSegmentContext(context.RemainingUrl, values);
            }

            if (!MatchSuffix(context.RemainingUrl))
                return false;

            return true;
        }

        protected virtual bool MatchSuffix(string remainingUrl)
        {
            if (Suffix == null)
                return String.IsNullOrEmpty(remainingUrl);

            return Suffix == remainingUrl;
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            //throw new NotImplementedException();
            return null;
        }
    }
}
