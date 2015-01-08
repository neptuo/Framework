using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Routing
{
    public interface IRouteParameterRegistry
    {
        IRouteParameterRegistry Add(string parameterName, IRouteParameter parameter);
        IRouteParameterRegistry Add(string parameterName, IRouteParameterFactory factory);
    }
}
