using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters
{
    /// <summary>
    /// A default implementation of <see cref="ISerializerContext"/>.
    /// </summary>
    public class DefaultSerializerContext : Generics.DefaultSerializerContext, ISerializerContext
    {
        public Type InputType { get; private set; }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="inputType">A type to serialize.</param>
        /// <param name="output">A serialization output.</param>
        public DefaultSerializerContext(Type inputType, Stream output)
            : base(output)
        {
            Ensure.NotNull(inputType, "inputType");
            InputType = inputType;
        }
    }
}
