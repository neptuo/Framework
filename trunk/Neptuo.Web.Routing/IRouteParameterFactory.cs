using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Neptuo.Web.Routing
{
    public interface IRouteParameterFactory
    {
        IRouteParameter CreateParameter(HttpContextBase httpContext);
    }
}
