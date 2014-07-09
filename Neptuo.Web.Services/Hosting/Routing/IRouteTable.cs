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
    /// Maps requests to pipelines.
    /// </summary>
    public interface IRouteTable
    {
        /// <summary>
        /// Adds new route rule with pipeline factory.
        /// </summary>
        /// <param name="route">New route to add.</param>
        /// <param name="pipelineFactory">Pipeline factory associated with <paramref name="route"/>.</param>
        void Add(IRoute route, IPipelineFactory pipelineFactory);

        /// <summary>
        /// Tries to find <paramref name="pipelineFactory"/> for <paramref name="request"/>.
        /// Returns <c>true</c> if pipeline is found; false otherwise.
        /// </summary>
        /// <param name="request">Current Htpp request.</param>
        /// <param name="pipelineFactory">Pipeline to handler <paramref name="request"/>.</param>
        /// <returns><c>true</c> if pipeline is found; false otherwise.</returns>
        bool TryGet(IHttpRequest request, out IPipelineFactory pipelineFactory);
    }
}
