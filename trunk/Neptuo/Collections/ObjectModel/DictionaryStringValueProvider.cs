using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Collections.ObjectModel
{
    [Obsolete]
    public class DictionaryStringValueProvider : IStringValueProvider<IDictionary<string, string>>
    {
        public string GetValue(IDictionary<string, string> model, string key)
        {
            if (model.ContainsKey(key))
                return model[key];

            return null;
        }
    }
}
