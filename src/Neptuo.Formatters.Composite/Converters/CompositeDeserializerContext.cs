using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters.Converters
{
    public class CompositeDeserializerContext
    {
        public ICompositeStorage Storage { get; private set; }
        public string Key { get; private set; }
        public Type ValueType { get; private set; }

        public CompositeDeserializerContext(ICompositeStorage storage, string key, Type valueType)
        {
            Ensure.NotNull(storage, "storage");
            Ensure.NotNullOrEmpty(key, "key");
            Ensure.NotNull(valueType, "valueType");
            Storage = storage;
            Key = key;
            ValueType = valueType;
        }
    }
}
