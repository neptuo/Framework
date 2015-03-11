using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Collections.Specialized
{
    /// <summary>
    /// Some common extensions.
    /// </summary>
    public static class _ReadOnlyKeyValueCollectionExtensions
    {
        /// <summary>
        /// Reads the value of <paramref name="key"/> in <paramref name="collection"/>.
        /// If value is found and can be converted to <typeparamref name="T"/>, returns it.
        /// Otherwise throws <see cref="InvalidOperationException"/>.
        /// </summary>
        /// <param name="collection">Collection of key-value pairs.</param>
        /// <param name="key">Requested key.</param>
        /// <param name="defaultValue">Optional default value if is not found or not convertible.</param>
        /// <returns>Value of <paramref name="key"/> in <paramref name="collection"/>.</returns>
        public static T Get<T>(this IReadOnlyKeyValueCollection collection, string key)
        {
            Ensure.NotNull(collection, "collection");

            T value;
            if (collection.TryGet(key, out value))
                return value;

            throw Ensure.Exception.InvalidOperation("Collection doesn't contain value of type '{0}' with key '{1}'.", typeof(T), key);
        }

        /// <summary>
        /// Reads the value of <paramref name="key"/> in <paramref name="collection"/>.
        /// If value is found and can be converted to <typeparamref name="T"/>, returns it.
        /// Otherwise returns <paramref name="defaultValue"/> (if provided) or throws <see cref="InvalidOperationException"/>.
        /// </summary>
        /// <param name="collection">Collection of key-value pairs.</param>
        /// <param name="key">Requested key.</param>
        /// <param name="defaultValue">Optional default value if is not found or not convertible.</param>
        /// <returns>Value of <paramref name="key"/> in <paramref name="collection"/>.</returns>
        public static T Get<T>(this IReadOnlyKeyValueCollection collection, string key, T? defaultValue)
            where T : struct
        {
            Ensure.NotNull(collection, "collection");

            T value;
            if (collection.TryGet(key, out value))
                return value;

            if (defaultValue != null)
                return defaultValue.Value;

            throw Ensure.Exception.InvalidOperation("Collection doesn't contain value of type '{0}' with key '{1}'.", typeof(T), key);
        }

        /// <summary>
        /// Reads the value of <paramref name="key"/> in <paramref name="collection"/>.
        /// If value is found and can be converted to <typeparamref name="T"/>, returns it.
        /// Otherwise returns <paramref name="defaultValue"/> (if provided) or throws <see cref="InvalidOperationException"/>.
        /// </summary>
        /// <param name="collection">Collection of key-value pairs.</param>
        /// <param name="key">Requested key.</param>
        /// <param name="defaultValue">Optional default value if is not found or not convertible.</param>
        /// <returns>Value of <paramref name="key"/> in <paramref name="collection"/>.</returns>
        public static T Get<T>(this IReadOnlyKeyValueCollection collection, string key, T defaultValue)
            where T : class
        {
            Ensure.NotNull(collection, "collection");

            T value;
            if (collection.TryGet(key, out value))
                return value;

            if (defaultValue != null)
                return defaultValue;

            throw Ensure.Exception.InvalidOperation("Collection doesn't contain value of type '{0}' with key '{1}'.", typeof(T), key);
        }

        /// <summary>
        /// Reads the value of <paramref name="key"/> in <paramref name="collection"/>.
        /// If value is found and can be converted to <see cref="System.Int32"/>, returns it.
        /// Otherwise returns <paramref name="defaultValue"/> (if provided) or throws <see cref="InvalidOperationException"/>.
        /// </summary>
        /// <param name="collection">Collection of key-value pairs.</param>
        /// <param name="key">Requested key.</param>
        /// <param name="defaultValue">Optional default value if is not found or not convertible.</param>
        /// <returns>Value of <paramref name="key"/> in <paramref name="collection"/>.</returns>
        public static int GetInt(this IReadOnlyKeyValueCollection collection, string key, int? defaultValue)
        {
            Ensure.NotNull(collection, "collection");

            int intValue;
            if (collection.TryGet(key, out intValue))
                return intValue;

            if (defaultValue != null)
                return defaultValue.Value;

            throw Ensure.Exception.InvalidOperation("Collection doesn't contain value of type int with key '{0}'.", key);
        }

        /// <summary>
        /// Reads the value of <paramref name="key"/> in <paramref name="collection"/>.
        /// If value is found and can be converted to <see cref="System.String"/>, returns it.
        /// Otherwise returns <paramref name="defaultValue"/> (if provided) or throws <see cref="InvalidOperationException"/>.
        /// </summary>
        /// <param name="collection">Collection of key-value pairs.</param>
        /// <param name="key">Requested key.</param>
        /// <param name="defaultValue">Optional default value if is not found or not convertible.</param>
        /// <returns>Value of <paramref name="key"/> in <paramref name="collection"/>.</returns>
        public static string GetString(this IReadOnlyKeyValueCollection collection, string key, string defaultValue)
        {
            Ensure.NotNull(collection, "collection");

            string stringValue;
            if (collection.TryGet(key, out stringValue))
                return stringValue;

            if (defaultValue != null)
                return defaultValue;

            throw Ensure.Exception.InvalidOperation("Collection doesn't contain value of type string with key '{0}'.", key);
        }

        /// <summary>
        /// Reads the value of <paramref name="key"/> in <paramref name="collection"/>.
        /// If value is found and can be converted to <see cref="System.Boolean"/>, returns it.
        /// Otherwise returns <paramref name="defaultValue"/> (if provided) or throws <see cref="InvalidOperationException"/>.
        /// </summary>
        /// <param name="collection">Collection of key-value pairs.</param>
        /// <param name="key">Requested key.</param>
        /// <param name="defaultValue">Optional default value if is not found or not convertible.</param>
        /// <returns>Value of <paramref name="key"/> in <paramref name="collection"/>.</returns>
        public static bool GetBool(this IReadOnlyKeyValueCollection collection, string key, bool? defaultValue)
        {
            Ensure.NotNull(collection, "collection");

            bool boolValue;
            if (collection.TryGet(key, out boolValue))
                return boolValue;

            if (defaultValue != null)
                return defaultValue.Value;

            throw Ensure.Exception.InvalidOperation("Collection doesn't contain value of type bool with key '{0}'.", key);
        }
    }
}