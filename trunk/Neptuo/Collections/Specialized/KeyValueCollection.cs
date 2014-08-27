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

        public void Set(string key, object value)
        {
            Guard.NotNull(key, "key");
            this[key] = value;
        }

        public bool TryGet(string key, out object value)
        {
            Guard.NotNull(key, "key");
            return TryGetValue(key, out value);
        }
    }
}
