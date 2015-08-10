using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Converters
{
    /// <summary>
    /// Repository for ceonverters between types.
    /// </summary>
    public interface IConverterRepository
    {
        /// <summary>
        /// Registers <paramref name="converter"/> for conversion from <paramref name="sourceType"/> to <paramref name="targetType"/>.
        /// </summary>
        /// <param name="sourceType">Source type.</param>
        /// <param name="targetType">Target type.</param>
        /// <param name="converter">Converter.</param>
        /// <returns>Self.</returns>
        IConverterRepository Add(Type sourceType, Type targetType, IConverter converter);

        /// <summary>
        /// Event used to find converter for unregistered pair of source and target type.
        /// </summary>
        event ConverterSearchDelegate OnSearchConverter;

        /// <summary>
        /// Tries to convert <paramref name="sourceValue"/> of type <typeparamref name="TSource"/> to target type <typeparamref name="TTarget"/>.
        /// </summary>
        /// <typeparam name="TSource">Source value type.</typeparam>
        /// <typeparam name="TTarget">Target value type.</typeparam>
        /// <param name="sourceValue">Source value.</param>
        /// <param name="targetValue">Output target value.</param>
        /// <returns>True if conversion was successfull.</returns>
        bool TryConvert<TSource, TTarget>(TSource sourceValue, out TTarget targetValue);

        /// <summary>
        /// Tries to convert <paramref name="sourceValue"/> to target type <paramref name="targetType"/>.
        /// </summary>
        /// <param name="sourceType">Type of source value.</param>
        /// <param name="targetType">Type of target value.</param>
        /// <param name="sourceValue">Source value.</param>
        /// <param name="targetValue">Output target value.</param>
        /// <returns>True if conversion was successfull.</returns>
        bool TryConvert(Type sourceType, Type targetType, object sourceValue, out object targetValue);
    }
}
