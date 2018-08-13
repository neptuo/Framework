using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters
{
    /// <summary>
    /// A contract for serializing and deserializing objects wich remembers source type.
    /// </summary>
    public interface IGenericFormatter : IGenericSerializer, IGenericDeserializer
    {
        
    }
}
