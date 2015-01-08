using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels
{
    /// <summary>
    /// Current value getter.
    /// </summary>
    public interface IModelValueGetter
    {
        /// <summary>
        /// Tries to read current value with <paramref name="identifier"/> as identifier.
        /// Returns <c>true</c> if value can be provided; false otherwise.
        /// </summary>
        /// <param name="identifier">Field identifier to read its value.</param>
        /// <param name="value">Current value for <paramref name="identifier"/>.</param>
        /// <returns><c>true</c> if value can be provided; false otherwise.</returns>
        bool TryGetValue(string identifier, out object value);
    }

    /// <summary>
    /// Provides extensions for reading values from <see cref="IModelValueGetter"/>
    /// </summary>
    public static class ModelValueGetterExtensions
    {
        /// <summary>
        /// Reads value from <paramref name="getter"/> with <paramref name="identifier"/> and if this can't be provided, returns <paramref name="defaultValue"/>.
        /// </summary>
        /// <returns>Value from <paramref name="getter"/> with <paramref name="identifier"/> and if this can't be provided, returns <paramref name="defaultValue"/>.</returns>
        public static object GetValueOrDefault(this IModelValueGetter getter, string identifier, object defaultValue)
        {
            object value;
            if (getter.TryGetValue(identifier, out value))
                return value;

            return defaultValue;
        }

        /// <summary>
        /// Reads value from <paramref name="getter"/> with <paramref name="identifier"/> and if this can't be provided, invokes <paramref name="defaultValueGetter"/>.
        /// </summary>
        /// <returns>Value from <paramref name="getter"/> with <paramref name="identifier"/> and if this can't be provided, invokes <paramref name="defaultValueGetter"/>.</returns>
        public static object GetValueOrDefault(this IModelValueGetter getter, string identifier, Func<object> defaultValueGetter)
        {
            object value;
            if (getter.TryGetValue(identifier, out value))
                return value;

            return defaultValueGetter();
        }

        /// <summary>
        /// Reads value from <paramref name="getter"/> with <paramref name="identifier"/> and if this can't be provided, returns <paramref name="defaultValue"/>.
        /// </summary>
        /// <returns>Value from <paramref name="getter"/> with <paramref name="identifier"/> and if this can't be provided, returns <paramref name="defaultValue"/>.</returns>
        public static T GetValueOrDefault<T>(this IModelValueGetter getter, string identifier, T defaultValue)
        {
            object value;
            if (getter.TryGetValue(identifier, out value))
                return (T)value;

            return defaultValue;
        }

        /// <summary>
        /// Reads value from <paramref name="getter"/> with <paramref name="identifier"/> and if this can't be provided, invokes <paramref name="defaultValueGetter"/>.
        /// </summary>
        /// <returns>Value from <paramref name="getter"/> with <paramref name="identifier"/> and if this can't be provided, invokes <paramref name="defaultValueGetter"/>.</returns>
        public static T GetValueOrDefault<T>(this IModelValueGetter getter, string identifier, Func<T> defaultValueGetter)
        {
            object value;
            if (getter.TryGetValue(identifier, out value))
                return (T)value;

            return defaultValueGetter();
        }
    }
}
