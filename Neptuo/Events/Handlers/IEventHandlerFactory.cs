using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Events.Handlers
{
    public interface IEventHandlerFactory<TEvent>
    {
        IEventHandler<TEvent> CreateHandler(TEvent eventData, IEventManager currentManager);
    }
}
