using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Commands
{
    [Serializable]
    public class CommandDispatcherException : CommandException
    {
        public CommandDispatcherException(string message, Exception inner) 
            : base(message, inner) 
        { }

        protected CommandDispatcherException(SerializationInfo info, StreamingContext context)
            : base(info, context) 
        { }
    }
}
