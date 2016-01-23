using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters
{
    /// <summary>
    /// Base composite exception.
    /// </summary>
    [Serializable]
    public class CompositeException : Exception
    {
        public CompositeException()
        { }

        public CompositeException(string message)
            : base(message)
        { }

        public CompositeException(string message, Exception inner)
            : base(message, inner)
        { }

        protected CompositeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}
