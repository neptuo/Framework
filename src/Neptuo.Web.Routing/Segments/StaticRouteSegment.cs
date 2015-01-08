using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Routing.Segments
{
    public class StaticRouteSegment : RequiredRouteSegment
    {
        protected string Path { get; private set; }

        public StaticRouteSegment(string path)
        {
            Path = path;
        }

        public override bool MatchRequiredUrl(RouteSegmentContext context)
        {
            if (context.OriginalUrl.StartsWith(Path))
            {
                string value = context.OriginalUrl.Substring(0, Path.Length);
                context.RemainingUrl = context.OriginalUrl.Substring(Path.Length);
                return true;
            }
            return false;
        }
    }
}
