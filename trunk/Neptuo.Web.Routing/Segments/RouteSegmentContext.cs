using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace Neptuo.Web.Routing.Segments
{
    public class RouteSegmentContext : IRouteParameterContext
    {
        public string OriginalUrl { get; private set; }
        public RouteValueDictionary Values { get; private set; }

        public string RemainingUrl { get; set; }

        public RouteSegmentContext(string originalUrl, RouteValueDictionary values)
        {
            OriginalUrl = originalUrl;
            Values = values;
            RemainingUrl = originalUrl;
        }
    }
}
