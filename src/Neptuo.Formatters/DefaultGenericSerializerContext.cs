using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters
{
    /// <summary>
    /// A default implementation of <see cref="IGenericSerializerContext"/>
    /// </summary>
    public class DefaultGenericSerializerContext : IGenericSerializerContext
    {
        public Stream Output { get; private set; }

        private KeyValueCollection metadata;

        public IKeyValueCollection Metadata
        {
            get
            {
                if (metadata == null)
                    metadata = new KeyValueCollection();

                return metadata;
            }
        }

        IReadOnlyKeyValueCollection IGenericSerializerContext.Metadata
        {
            get { return Metadata; }
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="output">A serialization output.</param>
        public DefaultGenericSerializerContext(Stream output)
        {
            Ensure.NotNull(output, "output");
            Output = output;
        }
    }
}
