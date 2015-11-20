using Neptuo.Services.Jobs.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Services.Jobs
{
    /// <summary>
    /// Registry for <see cref="IServiceHandler"/>.
    /// </summary>
    public class ServiceHandlerCollection : ServiceHandlerBase
    {
        private readonly List<IServiceHandler> services = new List<IServiceHandler>();

        /// <summary>
        /// Adds service.
        /// </summary>
        /// <param name="service">Service to add.</param>
        /// <returns>Self (for fluency).</returns>
        public ServiceHandlerCollection Add(IServiceHandler service)
        {
            Ensure.NotNull(service, "service");
            services.Add(service);
            return this;
        }

        protected override void OnStart()
        {
            foreach (IServiceHandler service in services)
                service.Start();
        }

        protected override void OnStop()
        {
            foreach (IServiceHandler service in services)
                service.Stop();
        }

        protected override void DisposeManagedResources()
        {
            bool wasRunning = IsRunning;
            if (wasRunning)
                Stop();

            foreach (IServiceHandler service in services)
            {
                if (wasRunning)
                    service.Stop();

                IDisposable disposableService = service as IDisposable;
                if (disposableService != null)
                    disposableService.Dispose();
            }

            base.DisposeManagedResources();
        }
    }
}
