using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Routing.Segments
{
    public class StaticNamedRouteSegment : NamedRouteSegment
    {
        protected string Path { get; private set; }

        public StaticNamedRouteSegment(string name, string path)
            : base(name)
        {
            Path = path;
        }

        public override bool MatchUrl(RouteSegmentContext context)
        {
            if (context.OriginalUrl.StartsWith(Path))
            {
                string value = context.OriginalUrl.Substring(0, Path.Length);
                context.Values[Name] = value;
                context.RemainingUrl = context.OriginalUrl.Substring(Path.Length);
                return true;
            }
            return false;
        }
    }
}
