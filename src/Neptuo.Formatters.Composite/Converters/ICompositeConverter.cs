using Neptuo.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters.Converters
{
    /// <summary>
    /// The converter contract for composite formatter.
    /// </summary>
    public interface ICompositeConverter : IConverter<CompositeDeserializerContext, object>, IConverter<CompositeSerializerContext, bool>
    { }
}
