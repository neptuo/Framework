using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters.Storages
{
    public class DefaultCompositeStorageFormatter : ICompositeStorageFormatter
    {
        public bool TryGet(ICompositeStorage storage, string key, Type valueType, out object value)
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
    }
}
