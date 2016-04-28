using Neptuo.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters.Converters
{
    /// <summary>
    /// The implementace of a 'two-way' (from source-to-target and from target-to-source) converter.
    /// </summary>
    public class TwoWayConverter<T1, T2> : IConverter<T1, T2>, IConverter<T2, T1>
    {
        private readonly OutFunc<T1, T2, bool> tryConvert1;
        private readonly OutFunc<T2, T1, bool> tryConvert2;
        
        /// <summary>
        /// Creates new instance that requires overriding both virtual methods.
        /// </summary>
        public TwoWayConverter()
        { }

        /// <summary>
        /// Creates new instance that converts by <paramref name="tryConvert"/>.
        /// </summary>
        /// <param name="tryConvert">Delegate for conversion of <typeparamref name="TSource"/> to <typeparamref name="TTarget"/>.</param>
        public TwoWayConverter(OutFunc<T1, T2, bool> tryConvert1, OutFunc<T2, T1, bool> tryConvert2)
        {
            Ensure.NotNull(tryConvert1, "tryConvert1");
            Ensure.NotNull(tryConvert2, "tryConvert2");
            this.tryConvert1 = tryConvert1;
            this.tryConvert2 = tryConvert2;
        }

        public virtual bool TryConvert(T1 sourceValue, out T2 targetValue)
        {
            if (tryConvert1 != null)
                return tryConvert1(sourceValue, out targetValue);

            throw Ensure.Exception.InvalidOperation("Override TryConvert method or provider the converter function.");
        }

        public virtual bool TryConvert(T2 sourceValue, out T1 targetValue)
        {
            if (tryConvert1 != null)
                return tryConvert2(sourceValue, out targetValue);

            throw Ensure.Exception.InvalidOperation("Override TryConvert method or provider the converter function.");
        }

        public bool TryConvert(Type sourceType, Type targetType, object sourceValue, out object targetValue)
        {
            Ensure.NotNull(sourceType, "sourceType");
            Ensure.NotNull(targetType, "targetType");

            if (sourceType == typeof(T1) && targetType == typeof(T2))
            {
                T2 target;
                if (TryConvert((T1)sourceValue, out target))
                {
                    targetValue = target;
                    return true;
                }
            }
            else if (sourceType == typeof(T2) && targetType == typeof(T1))
            {
                T1 target;
                if (TryConvert((T2)sourceValue, out target))
                {
                    targetValue = target;
                    return true;
                }
            }

            targetValue = null;
            return false;
        }
    }
}
