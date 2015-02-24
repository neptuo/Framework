using Neptuo.AppServices.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.AppServices.Hosting.Processing
{
    /// <summary>
    /// Wrapper which for each call to <see cref="IBackgroundHandler.Invoke"/> creates new instance of inner handler.
    /// </summary>
    public class TransientBackgroundHandler : IBackgroundHandler
    {
        private readonly IActivator<IBackgroundHandler> activator;

        /// <summary>
        /// Creaties new instance which for each call to <see cref="IBackgroundHandler.Invoke"/> 
        /// creates new instance of inner handler using <paramref name="activator"/>.
        /// </summary>
        /// <param name="activator">Activator for inner handler instances.</param>
        public TransientBackgroundHandler(IActivator<IBackgroundHandler> activator)
        {
            Guard.NotNull(activator, "activator");
            this.activator = activator;
        }

        public void Invoke()
        {
            activator.Create().Invoke();
        }
    }
}
