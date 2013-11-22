using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.BindingConverters
{
    public class BoolBindingConverter : BindingConverterBase<bool>
    {
        public override bool TryConvert(string sourceValue, IFieldDefinition targetField, out bool targetValue)
        {
            if (sourceValue == null)
            {
                targetValue = false;
                return false;
            }

            bool result;
            if (Boolean.TryParse(sourceValue, out result))
            {
                targetValue = result;
                return true;
            }

            if (sourceValue.ToLowerInvariant() == "on")
            {
                targetValue = true;
                return true;
            }

            targetValue = false;
            return false;
        }
    }
}
