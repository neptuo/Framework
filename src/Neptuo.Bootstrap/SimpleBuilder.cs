using Neptuo.Bootstrap.Handlers;
using Neptuo.Bootstrap.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap
{
    /// <summary>
    /// Simple bootstrapper builder.
    /// </summary>
    public class SimpleBuilder
    {
        /// <summary>
        /// Creates instance of manual handler loader.
        /// </summary>
        /// <returns>Configurated instance of <see cref="ManualLoaderBuilder"/>.</returns>
        public ManualLoaderBuilder ToManual()
        {
            return new ManualLoaderBuilder(new BootstrapHandlerExecutor());
        }

        /// <summary>
        /// Creates instance of automatic handler loader.
        /// </summary>
        /// <returns>Configurated instance of <see cref="AutomaticLoaderBuilder"/>.</returns>
        public AutomaticLoaderBuilder ToAutomatic()
        {
            return new AutomaticLoaderBuilder(new BootstrapHandlerExecutor());
        }


        private class BootstrapHandlerExecutor : IBootstrapHandlerExecutor
        {
            public void Execute(IBootstrapHandler handler)
            {
                handler.Handle();
            }
        }
    }
}
