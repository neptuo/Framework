using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters.Storages
{
    /// <summary>
    /// The implementation of <see cref="ICompositeStorageFormatter"/> that simply lets the underlaying storage to serialize/deserialize values.
    /// </summary>
    public class DefaultCompositeStorageFormatter : ICompositeStorageFormatter
    {
        public bool TryDeserialize(ICompositeStorage storage, string key, Type valueType, out object value)
        {
            object objectValue;
            if (!storage.TryGet(key, out objectValue))
            {
                value = null;
                return false;
            }

            value = objectValue;
            return true;
        }

        public bool TrySerialize(ICompositeStorage storage, string key, object value)
        {
            storage.Add(key, value);
            return true;
        }
    }
}
