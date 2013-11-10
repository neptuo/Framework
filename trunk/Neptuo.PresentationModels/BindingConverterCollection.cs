using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels
{
    public class BindingConverterCollection : IBindingConverterCollection
    {
        protected Dictionary<IFieldType, IBindingConverter> Storage { get; private set; }

        public BindingConverterCollection()
        {
            Storage = new Dictionary<IFieldType, IBindingConverter>();
        }

        public bool TryConvert(string sourceValue, IFieldDefinition targetField, out object targetValue)
        {
            IBindingConverter converter;
            if (Storage.TryGetValue(targetField.FieldType, out converter) && converter.TryConvert(sourceValue, targetField, out targetValue))
                return true;

            targetValue = null;
            return false;
        }
    }
}
