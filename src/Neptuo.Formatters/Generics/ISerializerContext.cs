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
    /// An context information for serializing objects using <see cref="ISerializer"/>.
    /// </summary>
    public interface ISerializerContext
    {
        /// <summary>
        /// An output for serialization.
        /// </summary>
        Stream Output { get; }

        /// <summary>
        /// A metadata of the context.
        /// </summary>
        IReadOnlyKeyValueCollection Metadata { get; }
    }
}
