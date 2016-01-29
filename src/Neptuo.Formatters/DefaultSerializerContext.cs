using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters
{
    /// <summary>
    /// Default implementation of <see cref="ISerializerContext"/>.
    /// </summary>
    public class DefaultSerializerContext : ISerializerContext
    {
        public Stream Output { get; private set; }
        public Type InputType { get; private set; }

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="inputType">The type to serialize.</param>
        /// <param name="output">The serialization output.</param>
        public DefaultSerializerContext(Type inputType, Stream output)
        {
            Ensure.NotNull(inputType, "inputType");
            Ensure.NotNull(output, "output");
            InputType = inputType;
            Output = output;
        }
    }
}
