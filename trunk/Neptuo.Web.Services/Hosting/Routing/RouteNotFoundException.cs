using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Services.Hosting.Routing
{
    /// <summary>
    /// Occured when route was not found to request
    /// </summary>
    [Serializable]
    public class RouteNotFoundException : RoutingException
    {
        public RouteNotFoundException(string method, string url)
            : base(String.Format("Route was not found for http method '{0}' on url '{1}'.", method, url))
        { }
    }
}
