using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Routing
{
    public interface IRouteParameterService
    {
        IRouteParameter Get(string parameterName);
    }
}
