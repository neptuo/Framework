using Neptuo;
using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Models.Keys
{
    public partial class KeyToParametersConverter : IKeyToParametersConverter
    {
        public MappingCollection Definitions { get; private set; }

        public KeyToParametersConverter()
            : this(new MappingCollection())
        { }

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
                parameters = new KeyCollection(parameters, prefix, null, false);

            return Add(parameters, key);
        }

        public IKeyToParametersConverter AddWithoutType(IKeyValueCollection parameters, IKey key)
        {
            Ensure.NotNull(parameters, "parameters");
            Ensure.NotNull(key, "key");

            parameters = new KeyCollection(parameters, null, null, true);
            return Add(parameters, key);
        }

        public IKeyToParametersConverter AddWithoutType(IKeyValueCollection parameters, string prefix, IKey key)
        {
            Ensure.NotNull(parameters, "parameters");
            Ensure.NotNull(key, "key");

            if (!string.IsNullOrEmpty(prefix))
                parameters = new KeyCollection(parameters, prefix, null, true);
            else
                parameters = new KeyCollection(parameters, null, null, true);

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
                parameters = new KeyProvider(parameters, prefix, null);

            return TryGet(parameters, out key);
        }

        public bool TryGetWithoutType<TKey>(IReadOnlyKeyValueCollection parameters, string keyType, out TKey key)
            where TKey : IKey
        {
            Ensure.NotNull(parameters, "parameters");
            parameters = new KeyProvider(parameters, null, keyType);

            return TryGet(parameters, out key);
        }

        public bool TryGetWithoutType<TKey>(IReadOnlyKeyValueCollection parameters, string keyType, string prefix, out TKey key)
            where TKey : IKey
        {
            Ensure.NotNull(parameters, "parameters");

            if (!string.IsNullOrEmpty(prefix))
                parameters = new KeyProvider(parameters, prefix, keyType);
            else
                parameters = new KeyProvider(parameters, null, keyType);

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
                parameters = new KeyProvider(parameters, prefix, null);

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

            parameters = new KeyProvider(parameters, null, keyType);
            return TryGet(parameters, type, out key);
        }

        public bool TryGetWithoutType(IReadOnlyKeyValueCollection parameters, string keyType, string prefix, out IKey key)
        {
            Ensure.NotNull(parameters, "parameters");

            Type type;
            if (!Definitions.keyTypeToClass.TryGetValue(keyType, out type))
                throw new MissingKeyTypeToKeyClassMappingException(keyType);

            if (!string.IsNullOrEmpty(prefix))
                parameters = new KeyProvider(parameters, prefix, keyType);
            else
                parameters = new KeyProvider(parameters, null, keyType);

            return TryGet(parameters, type, out key);
        }

        #endregion
    }
}
