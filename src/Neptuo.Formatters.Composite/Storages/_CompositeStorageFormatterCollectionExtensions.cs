using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters.Storages
{
    /// <summary>
    /// Common extensions for <see cref="CompositeStorageFormatterCollection"/>.
    /// </summary>
    public static class _CompositeStorageFormatterCollectionExtensions
    {
        public static CompositeStorageFormatterCollection AddCompositeModel(this CompositeStorageFormatterCollection collection)
        {
            Ensure.NotNull(collection, "collection");
            return collection.AddSearchHandler(TryGetCompositeModel);
        }

        private static bool TryGetCompositeModel(Type valueType, out ICompositeStorageFormatter formatter)
        {
            if (typeof(ICompositeModel).IsAssignableFrom(valueType))
            {
                formatter = new CompositeModelStorageFormatter();
                return true;
            }

            formatter = null;
            return false;
        }


        public static CompositeStorageFormatterCollection AddDefault(this CompositeStorageFormatterCollection collection)
        {
            Ensure.NotNull(collection, "collection");
            return collection.AddSearchHandler(TryGetDefault);
        }

        private static bool TryGetDefault(Type valueType, out ICompositeStorageFormatter formatter)
        {
            formatter = new DefaultCompositeStorageFormatter();
            return true;
        }
    }
}
