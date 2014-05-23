using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Events.Handlers
{
    public class GetterEventHandlerFactory<TEvent> : IEventHandlerFactory<TEvent>
    {
        protected Func<IEventHandler<TEvent>> Getter { get; private set; }

        public GetterEventHandlerFactory(Func<IEventHandler<TEvent>> getter)
        {
            if (getter == null)
                throw new ArgumentNullException("getter");

            Getter = getter;
        }

        public IEventHandler<TEvent> CreateHandler(TEvent eventData)
        {
            return Getter();
        }
    }
}
