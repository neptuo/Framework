using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters.Generics
{
    /// <summary>
    /// A contract for serializing and deserializing objects wich remembers source type.
    /// </summary>
    public interface IFormatter : ISerializer, IDeserializer
    {
        
    }
}
