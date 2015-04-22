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
    public abstract class TokenRouteBase : RouteBase
    {
        protected TokenParser TokenParser { get; private set; }
        protected IRouteParameterService ParameterService { get; private set; }
        protected abstract bool IsAbsolute { get; }

        public IRouteHandler RouteHandler { get; set; }
        public string Suffix { get; set; }
        public bool CaseSensitive { get; set; }

        public TokenRouteBase(IRouteHandler routeHandler, string suffix = null, bool caseSensitive = false, IRouteParameterService parameterService = null)
        {
            Ensure.NotNull(routeHandler, "routeHandler");
            CaseSensitive = caseSensitive;

            RouteHandler = routeHandler;
            Suffix = suffix;
            ParameterService = parameterService ?? RouteParameters.Service;
            TokenParser = CreateTokenParser();
        }

        protected virtual TokenParser CreateTokenParser()
        {
            TokenParser parser = new TokenParser();
            parser.Configuration.AllowTextContent = true;
            return parser;
        }

        protected virtual List<RouteSegment> BuildRoute(string routeUrl)
        {
            if (!CaseSensitive)
                routeUrl = routeUrl.ToLowerInvariant();

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
                throw Ensure.Exception.ArgumentOutOfRange("routeUrl", "Route url is not valid format for TokenRoute.");

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
            if (MatchUrl(requestUrl, routeData.Values))
                return routeData;

            return null;
        }

        protected virtual string GetRequestUrl(HttpContextBase httpContext)
        {
            return httpContext.Request.AppRelativeCurrentExecutionFilePath + httpContext.Request.PathInfo;
        }

        protected abstract bool MatchUrl(string url, RouteValueDictionary values);

        protected virtual bool MatchSuffix(string remainingUrl)
        {
            if (Suffix == null)
                return String.IsNullOrEmpty(remainingUrl);

            return Suffix == remainingUrl;
        }
    }
}
