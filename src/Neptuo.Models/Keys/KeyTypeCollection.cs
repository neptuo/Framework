using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Models.Keys
{
    public class KeyTypeCollection : IKeyTypeProvider
    {
        private readonly object storageLock = new object();
        private readonly Dictionary<string, Type> keyTypeToType = new Dictionary<string, Type>();
        private readonly Dictionary<Type, string> typeToKeyType = new Dictionary<Type, string>();

        public KeyTypeCollection Add(string keyType, Type type)
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
