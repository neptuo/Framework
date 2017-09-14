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
        private class KeyCollection : IKeyValueCollection
        {
            private readonly IKeyValueCollection inner;
            private readonly string prefix;
            private readonly string keyType;
            private readonly bool isTypeIgnored;

            public KeyCollection(IKeyValueCollection inner, string prefix, string keyType, bool isTypeIgnored)
            {
                Ensure.NotNull(inner, "inner");
                this.inner = inner;
                this.prefix = prefix;
                this.keyType = keyType;
                this.isTypeIgnored = isTypeIgnored;
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

            public IKeyValueCollection Add(string key, object value)
            {
                Ensure.NotNullOrEmpty(key, "key");

                if (isTypeIgnored && key == "Type")
                    return this;

                if (prefix != null)
                    key = prefix + key;

                inner.Add(key, value);
                return this;
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
