using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Collections.Specialized
{
    /// <summary>
    /// Extensible <see cref="IKeyValueCollection"/>.
    /// When reading value, which has not been set yet, calls registered provider.
    /// </summary>
    public class ProviderKeyValueCollection : KeyValueCollection
    {
        public bool IsReadOnly { get; set; }

        public IKeyValueCollection Set(string key, object value)
        {
            if (IsReadOnly)
                throw Guard.Exception.InvalidOperation("Collection is in read-only mode.");

            throw Guard.Exception.NotImplemented();
        }

        public bool TryGet<T>(string key, out T value)
        {
            throw Guard.Exception.NotImplemented();
        }
    }
}
