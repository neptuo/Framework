using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace Neptuo.Web.Routing
{
    public interface IRouteParameterContext
    {
        string OriginalUrl { get; }
        RouteValueDictionary Values { get; }

        string RemainingUrl { get; set; }
    }
}
