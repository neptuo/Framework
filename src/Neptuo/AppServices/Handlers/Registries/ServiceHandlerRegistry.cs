using Neptuo.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.AppServices.Handlers
{
    /// <summary>
    /// Registry for <see cref="IServiceHandler"/>.
    /// </summary>
    public class ServiceHandlerRegistry : DisposableBase, IServiceHandler
    {
        private readonly List<IServiceHandler> services = new List<IServiceHandler>();

        public bool IsRunning { get; private set; }

        /// <summary>
        /// Adds service described by <paramref name="service"/>.
        /// </summary>
        /// <param name="service">Descriptor of service to add.</param>
        /// <returns>Self (for fluency).</returns>
        public ServiceHandlerRegistry Add(IServiceHandler service)
        {
            Guard.NotNull(service, "service");
            services.Add(service);
            return this;
        }

        public void Start()
        {
            if (!IsRunning)
            {
                IsRunning = true;

                foreach (IServiceHandler service in services)
                    service.Start();
            }
        }

        public void Stop()
        {
            if (IsRunning)
            {
                IsRunning = false;

                foreach (IServiceHandler service in services)
                    service.Stop();
            }
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
