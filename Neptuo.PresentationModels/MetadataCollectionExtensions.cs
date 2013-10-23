using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels
{
    public static class MetadataCollectionExtensions
    {
        public static T GetOrDefault<T>(this IFieldMetadataCollection metadata, string key, T defaultValue)
        {
            object value;
            if (!metadata.TryGet(key, out value))
                return defaultValue;

            if (!(value is T))
                return defaultValue;

            return (T)defaultValue;
        }

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
