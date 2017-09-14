using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Models.Keys
{
    partial class KeyToParametersConverter
    {
        public class MappingCollection
        {
            internal readonly Dictionary<Type, Action<IKeyValueCollection, IKey>> keyToParameters = new Dictionary<Type, Action<IKeyValueCollection, IKey>>();
            internal readonly Dictionary<Type, OutFunc<IReadOnlyKeyValueCollection, IKey, bool>> parametersToKey = new Dictionary<Type, OutFunc<IReadOnlyKeyValueCollection, IKey, bool>>();
            internal readonly Dictionary<string, Type> keyTypeToClass = new Dictionary<string, Type>();

            public MappingCollection AddKeyToParameters<TKey>(Action<IKeyValueCollection, TKey> handler)
                where TKey : IKey
            {
                Ensure.NotNull(handler, "handler");
                keyToParameters[typeof(TKey)] = (parameters, key) => handler(parameters, (TKey)key);
                return this;
            }

            public MappingCollection AddParametersToKey<TKey>(OutFunc<IReadOnlyKeyValueCollection, TKey, bool> handler)
                where TKey : IKey
            {
                Ensure.NotNull(handler, "handler");
                parametersToKey[typeof(TKey)] = new OutFuncWrapper<TKey>(handler).TryGet;
                return this;
            }

            public MappingCollection AddKeyTypeToKeyClass(string keyType, Type keyClass)
            {
                Ensure.NotNullOrEmpty(keyType, "keyType");
                Ensure.NotNull(keyClass, "keyClass");

                if (!typeof(IKey).IsAssignableFrom(keyClass))
                    throw Ensure.Exception.ArgumentOutOfRange("keyClass", "Key class must implement '{0}'.", typeof(IKey).FullName);

                keyTypeToClass[keyType] = keyClass;
                return this;
            }

        }
    }
}
