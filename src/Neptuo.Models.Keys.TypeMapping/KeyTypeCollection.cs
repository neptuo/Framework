using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Models.Keys
{
    /// <summary>
    /// An implementation of <see cref="IKeyTypeProvider"/> with manual registration.
    /// </summary>
    public class KeyTypeCollection : IKeyTypeProvider
    {
        private readonly object storageLock = new object();
        private readonly Dictionary<string, Type> keyTypeToType = new Dictionary<string, Type>();
        private readonly Dictionary<Type, string> typeToKeyType = new Dictionary<Type, string>();

        /// <summary>
        /// Adds mapping from <paramref name="keyType"/> to <paramref name="type"/> and vice-versa.
        /// </summary>
        /// <param name="keyType">A <see cref="IKey.Type"/> string.</param>
        /// <param name="type">A clr type.</param>
        /// <returns>Self (for fluency).</returns>
        public KeyTypeCollection AddDual(string keyType, Type type)
        {
            Ensure.NotNullOrEmpty(keyType, "keyType");
            Ensure.NotNull(type, "type");

            lock (storageLock)
            {
                keyTypeToType[keyType] = type;
                typeToKeyType[type] = keyType;
            }

            return this;
        }

        /// <summary>
        /// Adds mapping from <paramref name="keyType"/> to <paramref name="type"/>.
        /// </summary>
        /// <param name="keyType">A <see cref="IKey.Type"/> string.</param>
        /// <param name="type">A clr type.</param>
        /// <returns>Self (for fluency).</returns>
        public KeyTypeCollection Add(string keyType, Type type)
        {
            Ensure.NotNullOrEmpty(keyType, "keyType");
            Ensure.NotNull(type, "type");

            lock (storageLock)
            {
                keyTypeToType[keyType] = type;
            }

            return this;
        }

        /// <summary>
        /// Adds mapping from <paramref name="type"/> to <paramref name="keyType"/>.
        /// </summary>
        /// <param name="type">A clr type.</param>
        /// <param name="keyType">A <see cref="IKey.Type"/> string.</param>
        /// <returns>Self (for fluency).</returns>
        public KeyTypeCollection Add(Type type, string keyType)
        {
            Ensure.NotNull(type, "type");
            Ensure.NotNullOrEmpty(keyType, "keyType");

            lock (storageLock)
            {
                keyTypeToType[keyType] = type;
            }

            return this;
        }

        public bool TryGet(string keyType, out Type type)
        {
            Ensure.NotNullOrEmpty(keyType, "keyType");
            return keyTypeToType.TryGetValue(keyType, out type);
        }

        public bool TryGet(Type type, out string keyType)
        {
            Ensure.NotNull(type, "type");
            return typeToKeyType.TryGetValue(type, out keyType);
        }
    }
}
