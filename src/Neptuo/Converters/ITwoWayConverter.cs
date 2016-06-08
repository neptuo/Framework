using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Converters
{
    public interface ITwoWayConverter<T1, T2> : IConverter
    {
        /// <summary>
        /// Tries to convert <paramref name="sourceValue"/>, of type <typeparamref name="TSource"/>, to <typeparamref name="TTarget"/>.
        /// </summary>
        /// <param name="sourceValue">Source value.</param>
        /// <param name="targetValue">Target value.</param>
        /// <returns><c>true</c> if <paramref name="sourceValue"/> can be converted to <typeparamref name="TTarget"/>; <c>false</c> otherwise.</returns>
        bool TryConvert1(T1 sourceValue, out T2 targetValue);
    }
}
