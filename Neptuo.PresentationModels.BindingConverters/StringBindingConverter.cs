using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.BindingConverters
{
    public class StringBindingConverter : IBindingConverter
    {
        public bool AllowConvertNull { get; set; }
        public bool AllowConvertEmpty { get; set; }
        public bool AllowConvertWhitespace { get; set; }

        public StringBindingConverter(bool allowConvertNull = false, bool allowConvertEmpty = true, bool allowConvertWhitespace = true)
        {
            AllowConvertNull = allowConvertNull;
            AllowConvertEmpty = allowConvertEmpty;
            AllowConvertWhitespace = allowConvertWhitespace;
        }

        public bool TryConvert(string sourceValue, IFieldDefinition targetField, out object targetValue)
        {
            if (!AllowConvertNull && sourceValue == null)
            {
                targetValue = null;
                return false;
            }

            if (!AllowConvertEmpty && String.IsNullOrEmpty(sourceValue))
            {
                targetValue = null;
                return false;
            }

            if (!AllowConvertWhitespace && String.IsNullOrWhiteSpace(sourceValue))
            {
                targetValue = null;
                return false;
            }

            targetValue = sourceValue;
            return true;
        }
    }
}
