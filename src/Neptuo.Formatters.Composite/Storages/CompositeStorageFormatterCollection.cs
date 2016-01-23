using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters.Storages
{
    public class CompositeStorageFormatterCollection : ICompositeStorageFormatter
    {
        private readonly Dictionary<Type, ICompositeStorageFormatter> formatters = new Dictionary<Type, ICompositeStorageFormatter>();
        private readonly OutFuncCollection<Type, ICompositeStorageFormatter, bool> onSearchFormatter = new OutFuncCollection<Type, ICompositeStorageFormatter, bool>();

        public CompositeStorageFormatterCollection Add(Type valueType, ICompositeStorageFormatter formatter)
        {
            Ensure.NotNull(valueType, "valueType");
            Ensure.NotNull(formatter, "formatter");
            formatters[valueType] = formatter;
            return this;
        }

        public CompositeStorageFormatterCollection AddSearchHandler(OutFunc<Type, ICompositeStorageFormatter, bool> handler)
        {
            Ensure.NotNull(handler, "handler");
            onSearchFormatter.Add(handler);
            return this;
        }

        public bool TryGet(ICompositeStorage storage, string key, Type valueType, out object value)
        {
            Ensure.NotNull(storage, "storage");
            Ensure.NotNullOrEmpty(key, "key");
            Ensure.NotNull(valueType, "valueType");

            ICompositeStorageFormatter formatter;
            if (formatters.TryGetValue(valueType, out formatter))
                return formatter.TryGet(storage, key, valueType, out value);

            if(onSearchFormatter.TryExecute(valueType, out formatter))
                return formatter.TryGet(storage, key, valueType, out value);

            value = null;
            return false;
        }
    }
}
