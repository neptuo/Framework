using Neptuo.Bootstrap.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap.Processing
{
    public class DefaultBootstrapper : IBootstrapper
    {
        private readonly IBootstrapHandlerExecutor handlerExecutor;
        private readonly IEnumerator<IBootstrapHandler> handlerEnumerator;

        public DefaultBootstrapper(IBootstrapHandlerExecutor handlerExecutor, IEnumerator<IBootstrapHandler> handlerEnumerator)
        {
            Ensure.NotNull(handlerExecutor, "handlerExecutor");
            Ensure.NotNull(handlerEnumerator, "handlerEnumerator");
            this.handlerExecutor = handlerExecutor;
            this.handlerEnumerator = handlerEnumerator;
        }

        public void Initialize()
        {
            while (handlerEnumerator.MoveNext())
            {
                IBootstrapHandler handler = handlerEnumerator.Current;
                handlerExecutor.Execute(handler);
            }
        }
    }
}
