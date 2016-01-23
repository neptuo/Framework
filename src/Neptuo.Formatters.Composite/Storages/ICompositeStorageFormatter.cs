using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters.Storages
{
    public interface ICompositeStorageFormatter
    {
        bool TryGet(ICompositeStorage storage, string key, Type valueType, out object value);

    }
}
