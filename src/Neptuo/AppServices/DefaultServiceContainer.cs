using Neptuo.AppServices.Handlers;
using Neptuo.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.AppServices
{
    /// <summary>
    /// Describes container for application services.
    /// </summary>
    public class DefaultServiceContainer : ServiceHandlerBase
    {
        private readonly IServiceHandler service;

        /// <summary>
        /// Creates new instance for service <paramref name="service"/>.
        /// </summary>
        /// <param name="service">Root service to manage by this container.</param>
        public DefaultServiceContainer(IServiceHandler service)
        {
            Guard.NotNull(service, "service");
            this.service = service;
        }

        protected override void OnStart()
        {
            service.Start();
        }

        protected override void OnStop()
        {
            service.Stop();
        }
    }
}
