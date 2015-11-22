using Neptuo.Jobs.Handlers;
using Neptuo.Jobs.Triggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Jobs
{
    /// <summary>
    /// Collection of <see cref="IBackgroundHandler"/> that are invoked by <see cref="IServiceTrigger"/>.
    /// </summary>
    public class BackgroundServiceCollection : ServiceHandlerBase
    {
        private readonly List<KeyValuePair<IServiceTrigger, IBackgroundHandler>> handlers = new List<KeyValuePair<IServiceTrigger, IBackgroundHandler>>();

        /// <summary>
        /// Adds background handler to the collection. <paramref name="handler"/> will be invoked on <paramref name="trigger"/>.
        /// </summary>
        /// <param name="trigger">Invocation trigger for <paramref name="handler"/>.</param>
        /// <param name="handler">Background handler to add.</param>
        /// <returns>Self (for fluency).</returns>
        public BackgroundServiceCollection AddHandler(IServiceTrigger trigger, IBackgroundHandler handler)
        {
            Ensure.NotNull(trigger, "trigger");
            Ensure.NotNull(handler, "worker");
            handlers.Add(new KeyValuePair<IServiceTrigger, IBackgroundHandler>(trigger, handler));
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
