using Neptuo;
using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Models.Keys
{
    /// <summary>
    /// A default implementation of <see cref="IKeyToParametersConverter"/>.
    /// It uses <see cref="Definitions"/> for mapping <see cref="IKey.Type"/> 
    /// to find implementations of <see cref="IKey"/> and to serializing/deserializing keys to parameters.
    /// </summary>
    public partial class KeyToParametersConverter : IKeyToParametersConverter
    {
        /// <summary>
        /// Gets a collection of mapping definitions.
        /// </summary>
        public MappingCollection Definitions { get; private set; }

        /// <summary>
        /// Creates a new instance with empty definition collection.
        /// </summary>
        public KeyToParametersConverter()
            : this(new MappingCollection())
        { }

        /// <summary>
        /// Creates a new instance with <paramref name="definitions"/>.
        /// </summary>
        /// <param name="definitions">A collection of mapping definitions.</param>
        public KeyToParametersConverter(MappingCollection definitions)
        {
            Ensure.NotNull(definitions, "definitions");
            Definitions = definitions;
        }

        #region Add key to parameters

        public IKeyToParametersConverter Add(IKeyValueCollection parameters, IKey key)
        {
            Ensure.NotNull(parameters, "parameters");
            Ensure.NotNull(key, "key");

            if (!key.IsEmpty)
            {
                Type keyType = key.GetType();
                Action<IKeyValueCollection, IKey> handler;
                if (!Definitions.keyToParameters.TryGetValue(keyType, out handler))
                    throw new MissingKeyClassToParametersMappingException(keyType);

                handler(parameters, key);
            }

            return this;
        }

        public IKeyToParametersConverter Add(IKeyValueCollection parameters, string prefix, IKey key)
        {
            Ensure.NotNull(parameters, "parameters");
            Ensure.NotNull(key, "key");

            if (!string.IsNullOrEmpty(prefix))
                parameters = new KeyValueCollectionWrapper(parameters, prefix, null, false);

            return Add(parameters, key);
        }

        public IKeyToParametersConverter AddWithoutType(IKeyValueCollection parameters, IKey key)
        {
            Ensure.NotNull(parameters, "parameters");
            Ensure.NotNull(key, "key");

            parameters = new KeyValueCollectionWrapper(parameters, null, null, true);
            return Add(parameters, key);
        }

        public IKeyToParametersConverter AddWithoutType(IKeyValueCollection parameters, string prefix, IKey key)
        {
            Ensure.NotNull(parameters, "parameters");
            Ensure.NotNull(key, "key");

            if (!string.IsNullOrEmpty(prefix))
                parameters = new KeyValueCollectionWrapper(parameters, prefix, null, true);
            else
                parameters = new KeyValueCollectionWrapper(parameters, null, null, true);

            return Add(parameters, key);
        }

        #endregion

        #region Get key from parameters

        private bool TryGet(IReadOnlyKeyValueCollection parameters, Type keyType, out IKey key)
        {
            OutFunc<IReadOnlyKeyValueCollection, IKey, bool> handler;
            if (!Definitions.parametersToKey.TryGetValue(keyType, out handler))
                throw new MissingParametersToKeyClassMappingException(keyType);

            return handler(parameters, out key);
        }

        public bool TryGet<TKey>(IReadOnlyKeyValueCollection parameters, out TKey key)
            where TKey : IKey
        {
            Ensure.NotNull(parameters, "parameters");

            Type keyType = typeof(TKey);
            IKey rawKey;
            if (TryGet(parameters, keyType, out rawKey))
            {
                key = (TKey)rawKey;
                return true;
            }

            key = default(TKey);
            return false;
        }

        public bool TryGet<TKey>(IReadOnlyKeyValueCollection parameters, string prefix, out TKey key)
            where TKey : IKey
        {
            Ensure.NotNull(parameters, "parameters");

            if (!string.IsNullOrEmpty(prefix))
                parameters = new KeyValueProviderWrapper(parameters, prefix, null);

            return TryGet(parameters, out key);
        }

        public bool TryGetWithoutType<TKey>(IReadOnlyKeyValueCollection parameters, string keyType, out TKey key)
            where TKey : IKey
        {
            Ensure.NotNull(parameters, "parameters");
            parameters = new KeyValueProviderWrapper(parameters, null, keyType);

            return TryGet(parameters, out key);
        }

        public bool TryGetWithoutType<TKey>(IReadOnlyKeyValueCollection parameters, string keyType, string prefix, out TKey key)
            where TKey : IKey
        {
            Ensure.NotNull(parameters, "parameters");

            if (!string.IsNullOrEmpty(prefix))
                parameters = new KeyValueProviderWrapper(parameters, prefix, keyType);
            else
                parameters = new KeyValueProviderWrapper(parameters, null, keyType);

            return TryGet(parameters, out key);
        }

        // These required mapping from IKey.Type to C# class.

        public bool TryGet(IReadOnlyKeyValueCollection parameters, out IKey key)
        {
            Ensure.NotNull(parameters, "parameters");

            string keyType = parameters.Get<string>("Type");
            Type type;
            if (!Definitions.keyTypeToClass.TryGetValue(keyType, out type))
                throw new MissingKeyTypeToKeyClassMappingException(keyType);

            return TryGet(parameters, type, out key);
        }

        public bool TryGet(IReadOnlyKeyValueCollection parameters, string prefix, out IKey key)
        {
            Ensure.NotNull(parameters, "parameters");

            if (!string.IsNullOrEmpty(prefix))
                parameters = new KeyValueProviderWrapper(parameters, prefix, null);

            string keyType = parameters.Get<string>("Type");
            Type type;
            if (!Definitions.keyTypeToClass.TryGetValue(keyType, out type))
                throw new MissingKeyTypeToKeyClassMappingException(keyType);

            return TryGet(parameters, type, out key);
        }

        public bool TryGetWithoutType(IReadOnlyKeyValueCollection parameters, string keyType, out IKey key)
        {
            Ensure.NotNull(parameters, "parameters");

            Type type;
            if (!Definitions.keyTypeToClass.TryGetValue(keyType, out type))
                throw new MissingKeyTypeToKeyClassMappingException(keyType);

            parameters = new KeyValueProviderWrapper(parameters, null, keyType);
            return TryGet(parameters, type, out key);
        }

        public bool TryGetWithoutType(IReadOnlyKeyValueCollection parameters, string keyType, string prefix, out IKey key)
        {
            Ensure.NotNull(parameters, "parameters");

            Type type;
            if (!Definitions.keyTypeToClass.TryGetValue(keyType, out type))
                throw new MissingKeyTypeToKeyClassMappingException(keyType);

            if (!string.IsNullOrEmpty(prefix))
                parameters = new KeyValueProviderWrapper(parameters, prefix, keyType);
            else
                parameters = new KeyValueProviderWrapper(parameters, null, keyType);

            return TryGet(parameters, type, out key);
        }

        #endregion
    }
}
