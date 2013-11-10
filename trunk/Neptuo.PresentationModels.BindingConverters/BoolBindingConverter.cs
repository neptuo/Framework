using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.BindingConverters
{
    public class BoolBindingConverter : IBindingConverter
    {
        public bool TryConvert(string sourceValue, IFieldDefinition targetField, out object targetValue)
        {
            if (sourceValue == null)
            {
                targetValue = null;
                return false;
            }

            bool result;
            if (Boolean.TryParse(sourceValue, out result))
            {
                targetValue = result;
                return true;
            }

            if(sourceValue.ToLowerInvariant() == "on")
            {
                targetValue = true;
                return true;
            }

            targetValue = null;
            return false;
        }
    }
}
