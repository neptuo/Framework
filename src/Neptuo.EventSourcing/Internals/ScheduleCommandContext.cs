using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Internals
{
    internal class ScheduleCommandContext
    {
        public HandlerDescriptor Handler { get; private set; }
        public ArgumentDescriptor Argument { get; private set; }
        public object Payload { get; private set; }

        public ScheduleCommandContext(HandlerDescriptor handler, ArgumentDescriptor argument, object payload)
        {
            Handler = handler;
            Argument = argument;
            Payload = payload;
        }
    }
}
