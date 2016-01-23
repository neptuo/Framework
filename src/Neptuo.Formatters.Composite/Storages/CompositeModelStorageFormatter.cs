using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters.Storages
{
    public class CompositeModelStorageFormatter : ICompositeStorageFormatter
    {
        public bool TryGet(ICompositeStorage storage, string key, Type valueType, out object value)
        {
            value = Activator.CreateInstance(valueType);
            ICompositeModel compositeModel = value as ICompositeModel;
            if (compositeModel == null)
            {
                value = null;
                return false;
            }

            ICompositeStorage modelStorage;
            if (storage.TryGet(key, out modelStorage))
            {
                compositeModel.Load(modelStorage);
                return true;
            }

            value = null;
            return false;
        }
    }
}
