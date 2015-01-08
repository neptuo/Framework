using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels
{
    /// <summary>
    /// Provides extensions for reading values from <see cref="IFieldMetadataCollection"/> and <see cref="IModelMetadataCollection"/>
    /// </summary>
    public static class MetadataCollectionExtensions
    {
        /// <summary>
        /// Reads value from <paramref name="metadata"/> with <paramref name="key"/> and if this can't be provided, returns <paramref name="defaultValue"/>.
        /// </summary>
        /// <returns>Value from <paramref name="metadata"/> with <paramref name="key"/> and if this can't be provided, returns <paramref name="defaultValue"/>.</returns>
        public static T GetOrDefault<T>(this IFieldMetadataCollection metadata, string key, T defaultValue)
        {
            object value;
            if (!metadata.TryGet(key, out value))
                return defaultValue;

            if (!(value is T))
                return defaultValue;

            return (T)defaultValue;
        }

        /// <summary>
        /// Reads value from <paramref name="metadata"/> with <paramref name="key"/> and if this can't be provided, returns <paramref name="defaultValue"/>.
        /// </summary>
        /// <returns>Value from <paramref name="metadata"/> with <paramref name="key"/> and if this can't be provided, returns <paramref name="defaultValue"/>.</returns>
        public static T GetOrDefault<T>(this IModelMetadataCollection metadata, string key, T defaultValue)
        {
            object value;
            if (!metadata.TryGet(key, out value))
                return defaultValue;

            if (!(value is T))
                return defaultValue;

            return (T)defaultValue;
        }
    }
}
