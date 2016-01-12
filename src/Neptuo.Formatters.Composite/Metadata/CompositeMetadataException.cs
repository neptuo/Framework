using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters.Metadata
{
    /// <summary>
    /// Base metadata related exception.
    /// </summary>
    [Serializable]
    public class CompositeMetadataException : Exception
    {
        public CompositeMetadataException() 
        { }

        public CompositeMetadataException(string message) 
            : base(message) 
        { }

        public CompositeMetadataException(string message, Exception inner) 
            : base(message, inner) 
        { }

        protected CompositeMetadataException(SerializationInfo info, StreamingContext context)
            : base(info, context) 
        { }
    }
}
