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
    /// An context information for serializing objects using <see cref="IGenericSerializer"/>.
    /// </summary>
    public interface IGenericSerializerContext
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
