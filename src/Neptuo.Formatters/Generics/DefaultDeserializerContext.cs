using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters.Generics
{
    /// <summary>
    /// A default implementation of <see cref="IGenericDeserializerContext"/>.
    /// </summary>
    public class DefaultDeserializerContext : IGenericDeserializerContext
    {
        public object Output { get; set; }

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

        IReadOnlyKeyValueCollection IGenericDeserializerContext.Metadata
        {
            get { return Metadata; }
        }
    }
}
