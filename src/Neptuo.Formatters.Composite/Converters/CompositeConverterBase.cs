using Neptuo.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters.Converters
{
    /// <summary>
    /// The class for writing objects to the <see cref="ICompositeStorage"/> and loading them from it.
    /// </summary>
    public abstract class CompositeConverterBase : ICompositeConverter
    {
        public bool TryConvert(CompositeDeserializerContext sourceValue, out object targetValue)
        {
            return TryDeserialize(sourceValue, out targetValue);
        }

        /// <summary>
        /// Tries to read value of type <paramref name="CompositeDeserializerContext.ValueType"/> with <paramref name="CompositeDeserializerContext.Key"/> from <paramref name="CompositeDeserializerContext.Storage"/>.
        /// </summary>
        /// <param name="context">The context about deserialization.</param>
        /// <param name="value">The deserialized value.</param>
        /// <returns><c>true</c> if loading value of type <paramref name="value"/> was successfull; <c>false</c> otherwise.</returns>
        protected abstract bool TryDeserialize(CompositeDeserializerContext context, out object value);

        public bool TryConvert(CompositeSerializerContext sourceValue, out bool targetValue)
        {
            if (TrySerialize(sourceValue))
                targetValue = true;
            else
                targetValue = false;

            return targetValue;
        }

        /// <summary>
        /// Tries to store <paramref name="CompositeSerializerContext.Value"/> to the <paramref name="CompositeSerializerContext.Storage"/> with the <paramref name="CompositeSerializerContext.Key"/>.
        /// </summary>
        /// <param name="context">The context about serialization.</param>
        /// <returns><c>true</c> if <paramref name="CompositeSerializerContext.Value"/> was successfully stored; <c>false</c> otherwise.</returns>
        protected abstract bool TrySerialize(CompositeSerializerContext context);

        public bool TryConvert(Type sourceType, Type targetType, object sourceValue, out object targetValue)
        {
            if (sourceType == typeof(CompositeDeserializerContext))
            {
                return TryConvert((CompositeDeserializerContext)sourceValue, out targetValue);
            }
            else if (sourceType == typeof(CompositeSerializerContext))
            {
                bool resultValue;
                bool result = TryConvert((CompositeSerializerContext)sourceValue, out resultValue);

                targetValue = resultValue;
                return result;
            }

            targetValue = null;
            return false;
        }
    }
}
