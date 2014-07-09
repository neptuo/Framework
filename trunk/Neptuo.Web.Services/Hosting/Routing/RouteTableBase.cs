using Neptuo.Web.Services.Hosting.Http;
using Neptuo.Web.Services.Hosting.Pipelines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Services.Hosting.Routing
{
    /// <summary>
    /// Stores route and pipeline factories in dictionary.
    /// </summary>
    public class RouteTableBase : IRouteTable
    {
        private Dictionary<IRoute, IPipelineFactory> storage = new Dictionary<IRoute, IPipelineFactory>();

        public void Add(IRoute route, IPipelineFactory pipelineFactory)
        {
            Guard.NotNull(route, "route");
            Guard.NotNull(pipelineFactory, "pipelineFactory");
            storage[route] = pipelineFactory;
        }

        public bool TryGet(IHttpRequest request, out IPipelineFactory pipelineFactory)
        {
            foreach (IRoute route in storage.Keys)
            {
                if (route.IsMatch(request))
                {
                    pipelineFactory = storage[route];
                    return true;
                }
            }

            throw new RouteNotFoundException(request.Method, request.Url.AbsolutePath);
        }
    }
}
