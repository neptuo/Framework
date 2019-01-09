using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels
{
    /// <summary>
    /// Simple wrap of <see cref="Dictionary{TKey, TValue}"/> that implements <see cref="IModelValueProvider"/>.
    /// </summary>
    public class DictionaryModelValueProvider : DisposableBase, IModelValueProvider
    {
        /// <summary>
        /// Gets a value storage.
        /// </summary>
        protected Dictionary<string, object> Storage { get; private set; }

        /// <summary>
        /// Creates new empty instance.
        /// </summary>
        public DictionaryModelValueProvider()
        {
            Storage = new Dictionary<string, object>();
        }

        public bool TryGetValue(string identifier, out object value)
        {
            Ensure.NotNull(identifier, "identifier");
            return Storage.TryGetValue(identifier, out value);
        }

        public bool TrySetValue(string identifier, object value)
        {
            Ensure.NotNull(identifier, "identifier");
            Storage[identifier] = value;
            return true;
        }
    }
}
