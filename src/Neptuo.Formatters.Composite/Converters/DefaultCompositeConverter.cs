using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters.Converters
{
    public class DefaultCompositeConverter : CompositeConverterBase
    {
        protected override bool TryDeserialize(CompositeDeserializerContext context, out object value)
        {
            // TODO: Fix loading value of type OBJECT (returns JValue)!
            object objectValue;
            if (!context.Storage.TryGet(context.Key, out objectValue))
            {
                value = null;
                return false;
            }

            value = objectValue;
            return true;
        }

        protected override bool TrySerialize(CompositeSerializerContext context)
        {
            context.Storage.Add(context.Key, context.Value);
            return true;
        }
    }
}
