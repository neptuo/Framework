using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels
{
    public class BindingDictionaryValueStorage : IBindingModelValueStorage
    {
        protected Dictionary<string, string> Storage { get; private set; }

        public BindingDictionaryValueStorage(Dictionary<string, string> storage = null)
        {
            if (storage == null)
                storage = new Dictionary<string,string>();

            Storage = storage;
        }

        public BindingDictionaryValueStorage Add(string key, string value)
        {
            Storage.Add(key, value);
            return this;
        }

        public bool TryGetValue(string identifier, out string targetValue)
        {
            if (identifier != null)
                return Storage.TryGetValue(identifier, out targetValue);

            targetValue = null;
            return false;
        }
    }
}
