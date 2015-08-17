using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap.Handlers
{
    /// <summary>
    /// Synchronous base class for implementation of <see cref="IBootstrapHandler"/>.
    /// </summary>
    public abstract class BootstrapHandlerBase : IBootstrapHandler
    {
        public Task HandleAsync()
        {
            Handle();
            return Task.FromResult(true);
        }

        /// <summary>
        /// Should process bootstrap step in synchronous manner.
        /// </summary>
        protected abstract void Handle();
    }
}
