using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neptuo.Internals
{
    internal class ScheduleEventContext
    {
        public IEnumerable<HandlerDescriptor> Handlers { get; private set; }
        public ArgumentDescriptor Argument { get; private set; }
        public object Payload { get; private set; }

        public ScheduleEventContext(IEnumerable<HandlerDescriptor> handlers, ArgumentDescriptor argument, object payload)
        {
            Handlers = handlers;
            Argument = argument;
            Payload = payload;
        }
    }
}
