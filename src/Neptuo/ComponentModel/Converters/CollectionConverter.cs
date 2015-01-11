using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.ComponentModel.Converters
{
    /// <summary>
    /// Converter for converting string value (splitted using defined separator) to one of supported collection types.
    /// </summary>
    /// <typeparam name="TItemTarget">Target collection item value.</typeparam>
    public class CollectionConverter<TItemTarget> : IConverter<string, TItemTarget>, IConverter<string, IEnumerable<TItemTarget>>, IConverter<string, List<TItemTarget>>
    {
        private readonly string separator;
        private readonly IConverter<string, TItemTarget> itemConverter;

        public CollectionConverter(string separator, IConverter<string, TItemTarget> itemConverter)
        {
            Guard.NotNullOrEmpty(separator, "separator");
            Guard.NotNull(itemConverter, "itemConverter");
            this.separator = separator;
            this.itemConverter = itemConverter;
        }

        protected IEnumerable<string> SplitSourceValue(string sourceValue)
        {
            if (String.IsNullOrEmpty(sourceValue))
                return Enumerable.Empty<string>();

            return sourceValue.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries);
        }

        protected bool TryConvertList(string sourceValue, out List<TItemTarget> targetValue)
        {
            bool hasError = false;
            List<TItemTarget> result = new List<TItemTarget>();
            IEnumerable<string> sourceValues = SplitSourceValue(sourceValue);
            foreach (string itemValue in sourceValues)
            {
                TItemTarget item;
                if (itemConverter.TryConvert(itemValue, out item))
                {
                    result.Add(item);
                }
                else
                {
                    hasError = true;
                    break;
                }
            }

            if (hasError)
                result = null;

            targetValue = result;
            return !hasError;
        }

        bool IConverter.TryConvertGeneral(Type sourceType, Type targetType, object sourceValue, out object targetValue)
        {
            if(sourceType != typeof(string))
            {
                targetValue = null;
                return false;
            }

            // Not generic typ return as list.
            if (sourceType != typeof(IEnumerable))
            {
                List<TItemTarget> result;
                bool success = TryConvertList((string)sourceValue, out result);
                targetValue = result;
                return success;
            }

            // If not generic or has more than one generic parameter or item can't be of type TItemTarget.
            if (!targetType.IsGenericType || targetType.GenericTypeArguments.Length != 1 || targetType.IsAssignableFrom(targetType.GenericTypeArguments[0]))
            {
                targetValue= null;
                return false;
            }

            Type genericType = targetType.GetGenericTypeDefinition();
            if (genericType.IsAssignableFrom(typeof(List<>)))
            {
                List<TItemTarget> result;
                bool success = TryConvertList((string)sourceValue, out result);
                targetValue = result;
                return success;
            }

            targetValue = null;
            return false;
        }

        bool IConverter<string, TItemTarget>.TryConvert(string sourceValue, out TItemTarget targetValue)
        {
            return itemConverter.TryConvert(sourceValue, out targetValue);
        }

        bool IConverter<string, IEnumerable<TItemTarget>>.TryConvert(string sourceValue, out IEnumerable<TItemTarget> targetValue)
        {
            List<TItemTarget> result;
            bool success = TryConvertList(sourceValue, out result);

            targetValue = result;
            return success;
        }

        bool IConverter<string, List<TItemTarget>>.TryConvert(string sourceValue, out List<TItemTarget> targetValue)
        {
            return TryConvertList(sourceValue, out targetValue);
        }
    }
}
