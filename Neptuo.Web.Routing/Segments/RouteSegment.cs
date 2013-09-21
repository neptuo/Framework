using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace Neptuo.Web.Routing.Segments
{
    public abstract class RouteSegment
    {
        public abstract bool MatchUrl(RouteSegmentContext context);
    }
}
