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
    public class DefaultServiceContainer : DisposableBase, IServiceHandler
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

        public bool IsRunning { get; private set; }

        public void Start()
        {
            if (!IsRunning)
            {
                IsRunning = true;
                service.Start();
            }
        }

        public void Stop()
        {
            if (IsRunning)
            {
                IsRunning = false;
                service.Stop();
            }
        }
    }
}
