using Neptuo.Events.Handlers;
using Neptuo.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Events
{
    public class EventData
    {
        public int Index { get; private set; }

        public EventData(int index)
        {
            Index = index;
        }
    }

    public class E1
    { }

    public class E2
    { }

    public class E3
    { }

    public class E1Handler : IEventHandler<E1>, IEventHandler<E2>, IEventHandler<E3>
    {
        public int E1Count { get; set; }
        public int E2Count { get; set; }
        public int E3Count { get; set; }

        public Task HandleAsync(E1 payload)
        {
            E1Count++;
            return Async.CompletedTask;
        }

        public Task HandleAsync(E2 payload)
        {
            E2Count++;
            return Async.CompletedTask;
        }

        public Task HandleAsync(E3 payload)
        {
            E3Count++;
            return Async.CompletedTask;
        }
    }

}
