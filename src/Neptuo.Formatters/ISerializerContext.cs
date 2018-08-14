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
    /// A context information for serializing objects using <see cref="ISerializer"/>.
    /// </summary>
    public interface ISerializerContext : Generics.ISerializerContext
    {
        /// <summary>
        /// A type to serialize.
        /// </summary>
        Type InputType { get; }
    }
}
