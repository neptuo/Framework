using Neptuo.AppServices.Handlers;
using Neptuo.AppServices.Triggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.AppServices
{
    public class WorkerServiceCollection : ServiceHandlerBase
    {
        private readonly List<KeyValuePair<IServiceTrigger, IBackgroundHandler>> handlers = new List<KeyValuePair<IServiceTrigger, IBackgroundHandler>>();

        /// <summary>
        /// Adds worker.
        /// </summary>
        /// <param name="trigger">Invocation trigger.</param>
        /// <param name="worker">Worker to add.</param>
        /// <returns>Self (for fluency).</returns>
        public WorkerServiceCollection AddHandler(IServiceTrigger trigger, IBackgroundHandler worker)
        {
            Ensure.NotNull(trigger, "trigger");
            Ensure.NotNull(worker, "worker");
            handlers.Add(new KeyValuePair<IServiceTrigger, IBackgroundHandler>(trigger, worker));
            return this;
        }

        protected override void OnStart()
        {
            foreach (KeyValuePair<IServiceTrigger, IBackgroundHandler> handler in handlers)
            {
                handler.Key.OnTrigger += handler.Value.Invoke;
                handler.Key.Start();
            }
        }

        protected override void OnStop()
        {
            foreach (KeyValuePair<IServiceTrigger, IBackgroundHandler> handler in handlers)
            {
                handler.Key.OnTrigger -= handler.Value.Invoke;
                handler.Key.Stop();
            }
        }

        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();

            foreach (KeyValuePair<IServiceTrigger, IBackgroundHandler> handler in handlers)
            {
                IDisposable disposableKey = handler.Key as IDisposable;
                if (disposableKey != null)
                    disposableKey.Dispose();

                IDisposable disposableValue = handler.Value as IDisposable;
                if (disposableValue != null)
                    disposableValue.Dispose();
            }
        }
    }
}
