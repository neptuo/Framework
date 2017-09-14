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
        private class KeyProvider : IReadOnlyKeyValueCollection
        {
            private readonly IReadOnlyKeyValueCollection inner;
            private readonly string prefix;
            private readonly string keyType;

            public KeyProvider(IReadOnlyKeyValueCollection inner, string prefix, string keyType)
            {
                Ensure.NotNull(inner, "inner");
                this.inner = inner;
                this.prefix = prefix;
                this.keyType = keyType;
            }

            public IEnumerable<string> Keys
            {
                get
                {
                    IEnumerable<string> keys = inner.Keys;
                    if (prefix == null && keyType == null)
                        return keys;

                    if (keyType != null)
                        keys = Enumerable.Concat(keys, new List<string>() { "Type" });

                    if (!string.IsNullOrEmpty(prefix))
                        keys = keys.Select(k => prefix + k);

                    return keys;
                }
            }
            
            public bool TryGet<T>(string key, out T value)
            {
                Ensure.NotNullOrEmpty(key, "key");

                if (keyType != null && key == "Type")
                {
                    value = (T)(object)keyType;
                    return true;
                }

                if (prefix != null)
                    key = prefix + key;

                return inner.TryGet(key, out value);
            }
        }
    }
}
