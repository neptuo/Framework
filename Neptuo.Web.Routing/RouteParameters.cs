using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

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
        private ConcurrentDictionary<string, IRouteParameterFactory> parameters = new ConcurrentDictionary<string, IRouteParameterFactory>();

        public IRouteParameterRegistry Add(string parameterName, IRouteParameter parameter)
        {
            if (parameterName == null)
                throw new ArgumentNullException("parameterName");

            if (parameter == null)
                throw new ArgumentNullException("parameter");

            parameters[parameterName] = new ProxyRouteParameterFactory(parameter);
            return this;
        }

        public IRouteParameterRegistry Add(string parameterName, IRouteParameterFactory factory)
        {
            if (parameterName == null)
                throw new ArgumentNullException("parameterName");

            if (factory == null)
                throw new ArgumentNullException("factory");

            parameters[parameterName] = factory;
            return this;
        }

        public IRouteParameter Get(string parameterName)
        {
            if (parameterName == null)
                throw new ArgumentNullException("parameterName");

            IRouteParameterFactory parameter;
            if (parameters.TryGetValue(parameterName, out parameter))
                return parameter.CreateParameter(new HttpContextWrapper(HttpContext.Current));

            parameterName = parameterName.ToLowerInvariant();
            if (parameters.TryGetValue(parameterName, out parameter))
                return parameter.CreateParameter(new HttpContextWrapper(HttpContext.Current));

            return new ProxyRouteParameter(parameterName);
        }
    }

    internal class ProxyRouteParameterFactory : IRouteParameterFactory
    {
        private IRouteParameter routeParameter;

        public ProxyRouteParameterFactory(IRouteParameter routeParameter)
        {
            this.routeParameter = routeParameter;
        }

        public IRouteParameter CreateParameter(HttpContextBase httpContext)
        {
            return routeParameter;
        }
    }

}
