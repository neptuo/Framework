using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Routing.Segments
{
    public abstract class NamedRouteSegment : RouteSegment
    {
        protected string Name { get; private set; }

        public NamedRouteSegment(string name)
        {
            Name = name;
        }
    }
}
