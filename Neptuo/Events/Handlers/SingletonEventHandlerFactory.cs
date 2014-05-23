using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Events.Handlers
{
    public class SingletonEventHandlerFactory<TEvent> : IEventHandlerFactory<TEvent>
    {
        protected IEventHandler<TEvent> Handler { get; private set; }

        public SingletonEventHandlerFactory(IEventHandler<TEvent> handler)
        {
            if (handler == null)
                throw new ArgumentNullException("handler");

            Handler = handler;
        }

        public IEventHandler<TEvent> CreateHandler(TEvent eventData)
        {
            return Handler;
        }
    }
}
