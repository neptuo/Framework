using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Collections.Specialized
{
    /// <summary>
    /// Base implementation of <see cref="IKeyValueCollection"/> using <see cref="Dictionary"/>.
    /// </summary>
    public class KeyValueCollection : Dictionary<string, object>, IKeyValueCollection
    {
        public KeyValueCollection()
        { }

        public KeyValueCollection(int capacity)
            : base(capacity)
        { }

        public KeyValueCollection(IEqualityComparer<string> comparer)
            :  base(comparer)
        { }

        public KeyValueCollection(IDictionary<string, object> source)
            : base(source)
        { }

        public IKeyValueCollection Set(string key, object value)
        {
            Guard.NotNull(key, "key");
            this[key] = value;
            return this;
        }

        public bool TryGet<T>(string key, out T value)
        {
            Guard.NotNull(key, "key");

            object sourceValue;
            if (TryGetValue(key, out sourceValue) && sourceValue != null)
            {
                if(sourceValue is T)
                {
                    value = (T)sourceValue;
                    return true;
                }

                object targetValue;
                if(Converts.Try(sourceValue.GetType(), typeof(T), sourceValue, out targetValue))
                {
                    value = (T)targetValue;
                    return true;
                }
            }

            value = default(T);
            return false;
        }
    }
}
