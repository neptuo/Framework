using Neptuo.Events.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Events
{
    public interface IEventRegistry
    {
        void Subscribe<TEvent>(IEventHandlerFactory<TEvent> factory);
        void UnSubscribe<TEvent>(IEventHandlerFactory<TEvent> factory);
    }
}
