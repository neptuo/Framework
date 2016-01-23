using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters.Storages
{
    /// <summary>
    /// The implementation of <see cref="ICompositeStorageFormatter"/> that requires value type to implement <see cref="ICompositeModel"/>
    /// and uses its <see cref="ICompositeModel.Save"/> and <see cref="ICompositeModel.Load"/> methods.
    /// </summary>
    public class CompositeModelStorageFormatter : ICompositeStorageFormatter
    {
        public bool TryDeserialize(ICompositeStorage storage, string key, Type valueType, out object value)
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
                if (modelStorage == null)
                    value = null;
                else
                    compositeModel.Load(modelStorage);

                return true;
            }

            value = null;
            return false;
        }

        public bool TrySerialize(ICompositeStorage storage, string key, object value)
        {
            if (value == null)
            {
                storage.Add(key, null);
                return true;
            }

            ICompositeModel compositeModel = value as ICompositeModel;
            if (compositeModel == null)
                return false;

            ICompositeStorage modelStorage = storage.Add(key);
            compositeModel.Save(modelStorage);
            return true;
        }
    }
}
