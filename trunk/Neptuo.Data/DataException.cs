using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Data
{
    [Serializable]
    public class DataException : Exception
    {
        public DataException() 
        { }

        public DataException(string message) 
            : base(message) 
        { }

        public DataException(string message, Exception inner) 
            : base(message, inner) 
        { }

        protected DataException(SerializationInfo info, StreamingContext context)
            : base(info, context) 
        { }
    }
}
