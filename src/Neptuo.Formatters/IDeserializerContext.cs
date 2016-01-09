using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters
{
    /// <summary>
    /// The context information for deserializing objects.
    /// </summary>
    public interface IDeserializerContext
    {
        /// <summary>
        /// The deserialized object.
        /// </summary>
        object Output { get; set; }
    }
}
