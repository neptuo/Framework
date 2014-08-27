using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Collections.Specialized
{
    public static class ReadOnlyKeyValueCollectionExtensions
    {
        public static bool TryGet(this IKeyValueCollection collection, string key, out int intValue)
        {
            Guard.NotNull(collection, "collection");

            object value;
            if(collection.TryGet(key, out value))
                return Converts.Try(value, out intValue);

            intValue = default(int);
            return false;
        }

        public static int Get(this IKeyValueCollection collection, string key, int? defaultValue)
        {
            int intValue;
            if (TryGet(collection, key, out intValue))
                return intValue;

            if (defaultValue != null)
                return defaultValue.Value;

            throw new InvalidOperationException(String.Format("Collection doesn't contain value of type int with key '{0}'", key));
        }

        public static bool TryGet(this IKeyValueCollection collection, string key, out string stringValue)
        {
            Guard.NotNull(collection, "collection");

            object value;
            if (collection.TryGet(key, out value))
                return Converts.Try(value, out stringValue);

            stringValue = default(string);
            return false;
        }

        public static string Get(this IKeyValueCollection collection, string key, string defaultValue)
        {
            string stringValue;
            if (TryGet(collection, key, out stringValue))
                return stringValue;

            if (defaultValue != null)
                return defaultValue;

            throw new InvalidOperationException(String.Format("Collection doesn't contain value of type string with key '{0}'", key));
        }

        public static bool TryGet(this IKeyValueCollection collection, string key, out bool boolValue)
        {
            Guard.NotNull(collection, "collection");

            object value;
            if (collection.TryGet(key, out value))
                return Converts.Try(value, out boolValue);

            boolValue = default(bool);
            return false;
        }

        public static bool Get(this IKeyValueCollection collection, string key, bool? defaultValue)
        {
            bool boolValue;
            if (TryGet(collection, key, out boolValue))
                return boolValue;

            if (defaultValue != null)
                return defaultValue.Value;

            throw new InvalidOperationException(String.Format("Collection doesn't contain value of type bool with key '{0}'", key));
        }
    }
}
