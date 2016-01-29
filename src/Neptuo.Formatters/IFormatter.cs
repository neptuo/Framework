using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters
{
    /// <summary>
    /// The constract for serializing and deserializing objects.
    /// </summary>
    public interface IFormatter : ISerializer, IDeserializer
    { }
}
