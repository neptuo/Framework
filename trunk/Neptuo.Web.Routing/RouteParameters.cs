using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Routing
{
    public class RouteParameters
    {
        private static IRouteParameterService service;
        private static IRouteParameterRegistry registry;

        public static IRouteParameterService Service
        {
            get { return service; }
        }

        public static IRouteParameterRegistry Registry
        {
            get { return registry; }
        }

        static RouteParameters()
        {
            var defaultService = new DefaultRouteParameterService();
            service = defaultService;
            registry = defaultService;
        }
    }

    internal class DefaultRouteParameterService : IRouteParameterService, IRouteParameterRegistry
    {
        private ConcurrentDictionary<string, IRouteParameter> parameters = new ConcurrentDictionary<string, IRouteParameter>();

        public void Add(string parameterName, IRouteParameter parameter)
        {
            if (parameterName == null)
                throw new ArgumentNullException("parameterName");

            if (parameter == null)
                throw new ArgumentNullException("parameter");

            parameters[parameterName] = parameter;
        }

        public IRouteParameter Get(string parameterName)
        {
            IRouteParameter parameter;
            if (parameters.TryGetValue(parameterName, out parameter))
                return parameter;

            return new ProxyRouteParameter(parameterName);
        }
    }

}
