using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.BindingConverters
{
    public abstract class BindingConverterBase<T> : IBindingConverter
    {
        public bool TryConvert(string sourceValue, IFieldDefinition targetField, out object targetValue)
        {
            T target;
            if (TryConvert(sourceValue, targetField, out target))
            {
                targetValue = target;
                return true;
            }

            targetValue = default(T);
            return false;
        }

        protected abstract bool TryConvert(string sourceValue, IFieldDefinition targetField, out T targetValue);
    }
}
