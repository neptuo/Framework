using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters.Converters
{
    public class CompositeSerializerContext
    {
        public ICompositeStorage Storage { get; private set; }
        public string Key { get; private set; }
        public object Value { get; private set; }

        public CompositeSerializerContext(ICompositeStorage storage, string key, object value)
        {
            Ensure.NotNull(storage, "storage");
            Ensure.NotNullOrEmpty(key, "key");
            Storage = storage;
            Key = key;
            Value = value;
        }
    }
}
