using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Models.Keys
{
    /// <summary>
    /// Common extensions for <see cref="KeyToParametersConverter"/>.
    /// </summary>
    public static class _KeyToParametersConverterExtensions
    {
        /// <summary>
        /// Gets a key from the <paramref name="parameters"/> or throws <see cref="MissingKeyInParametersException"/>.
        /// </summary>
        /// <typeparam name="TKey">A type of the key.</typeparam>
        /// <param name="converter">A key-parameters converter.</param>
        /// <param name="parameters">A collection of parameters.</param>
        /// <returns>A key from the <paramref name="parameters"/> or throws <see cref="MissingKeyInParametersException"/>.</returns>
        public static TKey Get<TKey>(this KeyToParametersConverter converter, IReadOnlyKeyValueCollection parameters) 
            where TKey : IKey
        {
            Ensure.NotNull(converter, "converter");
            TKey key;
            if (converter.TryGet(parameters, out key))
                return key;

            throw new MissingKeyInParametersException();
        }

        /// <summary>
        /// Gets a key from the <paramref name="parameters"/> with <paramref name="prefix"/> or throws <see cref="MissingKeyInParametersException"/>.
        /// </summary>
        /// <typeparam name="TKey">A type of the key.</typeparam>
        /// <param name="converter">A key-parameters converter.</param>
        /// <param name="parameters">A collection of parameters.</param>
        /// <param name="prefix">A values prefix for <paramref name="parameters"/>.</param>
        /// <returns>A key from the <paramref name="parameters"/> or throws <see cref="MissingKeyInParametersException"/>.</returns>
        public static TKey Get<TKey>(this KeyToParametersConverter converter, IReadOnlyKeyValueCollection parameters, string prefix) 
            where TKey : IKey
        {
            Ensure.NotNull(converter, "converter");
            TKey key;
            if (converter.TryGet(parameters, prefix, out key))
                return key;

            throw new MissingKeyInParametersException();
        }

        /// <summary>
        /// Gets a key from the <paramref name="parameters"/> with <see cref="IKey.Type"/> set to <paramref name="keyType"/> or throws <see cref="MissingKeyInParametersException"/>.
        /// </summary>
        /// <typeparam name="TKey">A type of the key.</typeparam>
        /// <param name="converter">A key-parameters converter.</param>
        /// <param name="parameters">A collection of parameters.</param>
        /// <param name="keyType">A <see cref="IKey.Type"/> for key to create.</param>
        /// <returns>A key from the <paramref name="parameters"/> or throws <see cref="MissingKeyInParametersException"/>.</returns>
        public static TKey GetWithoutType<TKey>(this KeyToParametersConverter converter, IReadOnlyKeyValueCollection parameters, string keyType) 
            where TKey : IKey
        {
            Ensure.NotNull(converter, "converter");
            TKey key;
            if (converter.TryGetWithoutType(parameters, keyType, out key))
                return key;

            throw new MissingKeyInParametersException();
        }

        public static Int16Key FindInt16KeyWithoutType(this KeyToParametersConverter converter, IReadOnlyKeyValueCollection parameters, string keyType)
        {
            Ensure.NotNull(converter, "converter");
            Int16Key key;
            if (converter.TryGetWithoutType(parameters, keyType, out key))
                return key;

            return Int16Key.Empty(keyType);
        }

        public static Int32Key FindInt32KeyWithoutType(this KeyToParametersConverter converter, IReadOnlyKeyValueCollection parameters, string keyType)
        {
            Ensure.NotNull(converter, "converter");
            Int32Key key;
            if (converter.TryGetWithoutType(parameters, keyType, out key))
                return key;

            return Int32Key.Empty(keyType);
        }

        public static Int64Key FindInt64KeyWithoutType(this KeyToParametersConverter converter, IReadOnlyKeyValueCollection parameters, string keyType)
        {
            Ensure.NotNull(converter, "converter");
            Int64Key key;
            if (converter.TryGetWithoutType(parameters, keyType, out key))
                return key;

            return Int64Key.Empty(keyType);
        }

        public static StringKey FindStringKeyWithoutType(this KeyToParametersConverter converter, IReadOnlyKeyValueCollection parameters, string keyType)
        {
            Ensure.NotNull(converter, "converter");
            StringKey key;
            if (converter.TryGetWithoutType(parameters, keyType, out key))
                return key;

            return StringKey.Empty(keyType);
        }

        public static GuidKey FindGuidKeyWithoutType(this KeyToParametersConverter converter, IReadOnlyKeyValueCollection parameters, string keyType)
        {
            Ensure.NotNull(converter, "converter");
            GuidKey key;
            if (converter.TryGetWithoutType(parameters, keyType, out key))
                return key;

            return GuidKey.Empty(keyType);
        }

        /// <summary>
        /// Gets a key from the <paramref name="parameters"/> with <paramref name="prefix"/> and <see cref="IKey.Type"/> set to <paramref name="keyType"/> or throws <see cref="MissingKeyInParametersException"/>.
        /// </summary>
        /// <typeparam name="TKey">A type of the key.</typeparam>
        /// <param name="converter">A key-parameters converter.</param>
        /// <param name="parameters">A collection of parameters.</param>
        /// <param name="prefix">A values prefix for <paramref name="parameters"/>.</param>
        /// <param name="keyType">A <see cref="IKey.Type"/> for key to create.</param>
        /// <returns>A key from the <paramref name="parameters"/> or throws <see cref="MissingKeyInParametersException"/>.</returns>
        public static TKey GetWithoutType<TKey>(this KeyToParametersConverter converter, IReadOnlyKeyValueCollection parameters, string keyType, string prefix) 
            where TKey : IKey
        {
            Ensure.NotNull(converter, "converter");
            TKey key;
            if (converter.TryGetWithoutType(parameters, keyType, prefix, out key))
                return key;

            throw new MissingKeyInParametersException();
        }

        public static Int16Key FindInt16KeyWithoutType(this KeyToParametersConverter converter, IReadOnlyKeyValueCollection parameters, string keyType, string prefix)
        {
            Ensure.NotNull(converter, "converter");
            Int16Key key;
            if (converter.TryGetWithoutType(parameters, keyType, prefix, out key))
                return key;

            return Int16Key.Empty(keyType);
        }

        public static Int32Key FindInt32KeyWithoutType(this KeyToParametersConverter converter, IReadOnlyKeyValueCollection parameters, string keyType, string prefix)
        {
            Ensure.NotNull(converter, "converter");
            Int32Key key;
            if (converter.TryGetWithoutType(parameters, keyType, prefix, out key))
                return key;

            return Int32Key.Empty(keyType);
        }

        public static Int64Key FindInt64KeyWithoutType(this KeyToParametersConverter converter, IReadOnlyKeyValueCollection parameters, string keyType, string prefix)
        {
            Ensure.NotNull(converter, "converter");
            Int64Key key;
            if (converter.TryGetWithoutType(parameters, keyType, prefix, out key))
                return key;

            return Int64Key.Empty(keyType);
        }

        public static StringKey FindStringKeyWithoutType(this KeyToParametersConverter converter, IReadOnlyKeyValueCollection parameters, string keyType, string prefix)
        {
            Ensure.NotNull(converter, "converter");
            StringKey key;
            if (converter.TryGetWithoutType(parameters, keyType, prefix, out key))
                return key;

            return StringKey.Empty(keyType);
        }

        public static GuidKey FindGuidKeyWithoutType(this KeyToParametersConverter converter, IReadOnlyKeyValueCollection parameters, string keyType, string prefix)
        {
            Ensure.NotNull(converter, "converter");
            GuidKey key;
            if (converter.TryGetWithoutType(parameters, keyType, prefix, out key))
                return key;

            return GuidKey.Empty(keyType);
        }

        /// <summary>
        /// Gets a key from the <paramref name="parameters"/> or throws <see cref="MissingKeyInParametersException"/>.
        /// </summary>
        /// <param name="converter">A key-parameters converter.</param>
        /// <param name="parameters">A collection of parameters.</param>
        /// <returns>A key from the <paramref name="parameters"/> or throws <see cref="MissingKeyInParametersException"/>.</returns>
        public static IKey Get(this KeyToParametersConverter converter, IReadOnlyKeyValueCollection parameters)
        {
            Ensure.NotNull(converter, "converter");
            IKey key;
            if (converter.TryGet(parameters, out key))
                return key;

            throw new MissingKeyInParametersException();
        }

        /// <summary>
        /// Gets a key from the <paramref name="parameters"/> with <paramref name="prefix"/> or throws <see cref="MissingKeyInParametersException"/>.
        /// </summary>
        /// <param name="converter">A key-parameters converter.</param>
        /// <param name="parameters">A collection of parameters.</param>
        /// <param name="prefix">A values prefix for <paramref name="parameters"/>.</param>
        /// <returns>A key from the <paramref name="parameters"/> or throws <see cref="MissingKeyInParametersException"/>.</returns>
        public static IKey Get(this KeyToParametersConverter converter, IReadOnlyKeyValueCollection parameters, string prefix)
        {
            Ensure.NotNull(converter, "converter");
            IKey key;
            if (converter.TryGet(parameters, prefix, out key))
                return key;

            throw new MissingKeyInParametersException();
        }

        /// <summary>
        /// Gets a key from the <paramref name="parameters"/> with <see cref="IKey.Type"/> set to <paramref name="keyType"/> or throws <see cref="MissingKeyInParametersException"/>.
        /// </summary>
        /// <param name="converter">A key-parameters converter.</param>
        /// <param name="parameters">A collection of parameters.</param>
        /// <param name="keyType">A <see cref="IKey.Type"/> for key to create.</param>
        /// <returns>A key from the <paramref name="parameters"/> or throws <see cref="MissingKeyInParametersException"/>.</returns>
        public static IKey GetWithoutType(this KeyToParametersConverter converter, IReadOnlyKeyValueCollection parameters, string keyType)
        {
            Ensure.NotNull(converter, "converter");
            IKey key;
            if (converter.TryGetWithoutType(parameters, keyType, out key))
                return key;

            throw new MissingKeyInParametersException();
        }

        /// <summary>
        /// Gets a key from the <paramref name="parameters"/> with <paramref name="prefix"/> and <see cref="IKey.Type"/> set to <paramref name="keyType"/> or throws <see cref="MissingKeyInParametersException"/>.
        /// </summary>
        /// <param name="converter">A key-parameters converter.</param>
        /// <param name="parameters">A collection of parameters.</param>
        /// <param name="prefix">A values prefix for <paramref name="parameters"/>.</param>
        /// <param name="keyType">A <see cref="IKey.Type"/> for key to create.</param>
        /// <returns>A key from the <paramref name="parameters"/> or throws <see cref="MissingKeyInParametersException"/>.</returns>
        public static IKey GetWithoutType(this KeyToParametersConverter converter, IReadOnlyKeyValueCollection parameters, string keyType, string prefix)
        {
            Ensure.NotNull(converter, "converter");
            IKey key;
            if (converter.TryGetWithoutType(parameters, keyType, prefix, out key))
                return key;

            throw new MissingKeyInParametersException();
        }
    }
}
