using Neptuo.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.AppServices.Handlers
{
    /// <summary>
    /// Base implementation of <see cref="IServiceHandler"/>.
    /// </summary>
    public abstract class ServiceHandlerBase : DisposableBase, IServiceHandler
    {
        public bool IsRunning { get; private set; }

        /// <summary>
        /// Invoked when service is starting from stopped state.
        /// </summary>
        protected abstract void OnStart();

        public void Start()
        {
            if (!IsDisposed && !IsRunning)
            {
                OnStart();
                IsRunning = true;
            }
        }

        /// <summary>
        /// Invoked when service is stopping from running state.
        /// </summary>
        protected abstract void OnStop();

        public void Stop()
        {
            if (!IsDisposed && IsRunning)
            {
                OnStop();
                IsRunning = false;
            }
        }
    }
}
