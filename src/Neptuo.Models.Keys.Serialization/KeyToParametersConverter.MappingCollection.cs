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
        /// <summary>
        /// A storage for mapping used by the <see cref="KeyToParametersConverter"/>.
        /// </summary>
        public class MappingCollection
        {
            internal readonly Dictionary<Type, Action<IKeyValueCollection, IKey>> keyToParameters = new Dictionary<Type, Action<IKeyValueCollection, IKey>>();
            internal readonly Dictionary<Type, OutFunc<IReadOnlyKeyValueCollection, IKey, bool>> parametersToKey = new Dictionary<Type, OutFunc<IReadOnlyKeyValueCollection, IKey, bool>>();
            internal readonly Dictionary<string, Type> keyTypeToClass = new Dictionary<string, Type>();

            /// <summary>
            /// Adds <paramref name="handler"/> to be used when serializing key of type <typeparamref name="TKey"/> to parameters collection.
            /// </summary>
            /// <typeparam name="TKey">A type of the key.</typeparam>
            /// <param name="handler">A handler to be used.</param>
            /// <returns>Self (for fluency).</returns>
            public MappingCollection AddKeyToParameters<TKey>(Action<IKeyValueCollection, TKey> handler)
                where TKey : IKey
            {
                Ensure.NotNull(handler, "handler");
                keyToParameters[typeof(TKey)] = (parameters, key) => handler(parameters, (TKey)key);
                return this;
            }

            /// <summary>
            /// Adds <paramref name="handler"/> to be when trying to create a key of the <typeparamref name="TKey"/> from parameters collection.
            /// </summary>
            /// <typeparam name="TKey">A type of the key.</typeparam>
            /// <param name="handler">A handler to be used.</param>
            /// <returns>Self (for fluency).</returns>
            public MappingCollection AddParametersToKey<TKey>(OutFunc<IReadOnlyKeyValueCollection, TKey, bool> handler)
                where TKey : IKey
            {
                Ensure.NotNull(handler, "handler");
                parametersToKey[typeof(TKey)] = new OutFuncWrapper<TKey>(handler).TryGet;
                return this;
            }

            /// <summary>
            /// Adds mapping from <paramref name="keyType" /> to <paramref name="keyClass"/> 
            /// when using non-generic methods for creating key from parameters collection.
            /// </summary>
            /// <param name="keyType">A <see cref="IKey.Type"/> value.</param>
            /// <param name="keyClass">An implementation of <see cref="IKey"/> to use for keys with type <paramref name="keyType"/>.</param>
            /// <returns>Self (for fluency).</returns>
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
