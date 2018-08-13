using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters
{
    /// <summary>
    /// A default implementation of <see cref="IDeserializerContext"/>.
    /// </summary>
    public class DefaultDeserializerContext : DefaultGenericDeserializerContext, IDeserializerContext
    {
        public Type OutputType { get; private set; }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="outputType">A type that is required to deserialize.</param>
        public DefaultDeserializerContext(Type outputType)
        {
            Ensure.NotNull(outputType, "outputType");
            OutputType = outputType;
        }
    }
}
