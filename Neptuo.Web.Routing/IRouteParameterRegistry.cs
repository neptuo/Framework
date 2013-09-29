using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Routing
{
    public interface IRouteParameterRegistry
    {
        void Add(string parameterName, IRouteParameter parameter);
        void Add(string parameterName, IRouteParameterFactory factory);
    }
}
