using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters
{
    /// <summary>
    /// A context information for deserializing objects using <see cref="IDeserializer"/>.
    /// </summary>
    public interface IDeserializerContext : Generics.IGenericDeserializerContext
    {
        /// <summary>
        /// An type that is required to deserialize.
        /// </summary>
        Type OutputType { get; }
    }
}
