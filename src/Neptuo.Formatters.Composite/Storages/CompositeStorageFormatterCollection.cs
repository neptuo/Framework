using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters.Storages
{
    /// <summary>
    /// The implementation of <see cref="ICompositeStorageFormatter"/> that enables to register formatters for concrete types
    /// and register search handler.
    /// </summary>
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

        private ICompositeStorageFormatter FindFormatter(Type valueType)
        {
            ICompositeStorageFormatter formatter;
            if (!formatters.TryGetValue(valueType, out formatter))
                onSearchFormatter.TryExecute(valueType, out formatter);

            return formatter;
        }

        public bool TryDeserialize(ICompositeStorage storage, string key, Type valueType, out object value)
        {
            Ensure.NotNull(storage, "storage");
            Ensure.NotNullOrEmpty(key, "key");
            Ensure.NotNull(valueType, "valueType");

            ICompositeStorageFormatter formatter = FindFormatter(valueType);
            if (formatter == null)
            {
                value = null;
                return false;
            }

            return formatter.TryDeserialize(storage, key, valueType, out value);
        }

        public bool TrySerialize(ICompositeStorage storage, string key, object value)
        {
            Ensure.NotNull(storage, "storage");
            Ensure.NotNullOrEmpty(key, "key");

            if (value == null)
            {
                storage.Add(key, null);
                return true;
            }

            Type valueType = value.GetType();
            ICompositeStorageFormatter formatter = FindFormatter(valueType);
            if (formatter == null)
            {
                value = null;
                return false;
            }

            return formatter.TrySerialize(storage, key, value);
        }
    }
}
