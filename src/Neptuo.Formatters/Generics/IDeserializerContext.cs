using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters.Generics
{
    /// <summary>
    /// A context information for deserializing objects using <see cref="IDeserializer"/>.
    /// </summary>
    public interface IDeserializerContext
    {
        /// <summary>
        /// A deserialized object.
        /// </summary>
        object Output { get; set; }

        /// <summary>
        /// An metadata of the context.
        /// </summary>
        IReadOnlyKeyValueCollection Metadata { get; }
    }
}
