using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Routing.Segments
{
    public abstract class RequiredRouteSegment : RouteSegment
    {
        public override bool MatchUrl(RouteSegmentContext context)
        {
            if (String.IsNullOrEmpty(context.OriginalUrl))
                return false;

            return MatchRequiredUrl(context);
        }

        public abstract bool MatchRequiredUrl(RouteSegmentContext context);
    }
}
