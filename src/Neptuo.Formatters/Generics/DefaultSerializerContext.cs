using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters.Generics
{
    /// <summary>
    /// A default implementation of <see cref="ISerializerContext"/>
    /// </summary>
    public class DefaultSerializerContext : ISerializerContext
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

        IReadOnlyKeyValueCollection ISerializerContext.Metadata
        {
            get { return Metadata; }
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="output">A serialization output.</param>
        public DefaultSerializerContext(Stream output)
        {
            Ensure.NotNull(output, "output");
            Output = output;
        }
    }
}
