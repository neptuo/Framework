using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Services.Commands
{
    [Serializable]
    public class CommandException : Exception
    {
        public CommandException()
        { }

        public CommandException(string message)
            : base(message)
        { }

        public CommandException(string message, Exception inner)
            : base(message, inner)
        { }

        protected CommandException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}
