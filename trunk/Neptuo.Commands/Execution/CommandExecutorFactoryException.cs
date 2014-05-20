using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Commands.Execution
{
    [Serializable]
    public class CommandExecutorFactoryException : CommandException
    {
        public CommandExecutorFactoryException() 
        { }
        
        public CommandExecutorFactoryException(string message) : base(message) 
        { }
        
        public CommandExecutorFactoryException(string message, Exception inner) : base(message, inner) 
        { }
        
        protected CommandExecutorFactoryException(SerializationInfo info, StreamingContext context)
            : base(info, context) 
        { }
    }
}
