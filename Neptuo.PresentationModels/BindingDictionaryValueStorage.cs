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

        public string GetValue(string identifier)
        {
            if (identifier != null)
            {
                string value;
                if (Storage.TryGetValue(identifier, out value))
                    return value;
            }
            return null;
        }
    }
}
