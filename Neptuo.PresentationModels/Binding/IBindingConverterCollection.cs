using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.Binding
{
    public interface IBindingConverterCollection
    {
        bool TryConvert(string sourceValue, IFieldDefinition targetField, out object targetValue);
    }
}
