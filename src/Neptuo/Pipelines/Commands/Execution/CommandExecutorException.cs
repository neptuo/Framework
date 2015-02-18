using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Pipelines.Commands.Execution
{
    [Serializable]
    public class CommandExecutorException : CommandException
    {
        public CommandExecutorException() 
        { }
        
        public CommandExecutorException(string message) : base(message) 
        { }
        
        public CommandExecutorException(string message, Exception inner) : base(message, inner) 
        { }
        
        protected CommandExecutorException(SerializationInfo info, StreamingContext context)
            : base(info, context) 
        { }
    }
}
