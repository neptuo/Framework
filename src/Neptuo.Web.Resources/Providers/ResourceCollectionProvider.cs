using Neptuo.Web.Resources.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Resources.Providers
{
    /// <summary>
    /// Provides resource collection.
    /// This class is used once when initializing <see cref="ResourceTable.Resources"/>.
    /// <see cref="ResourceCollectionProvider.ProviderGetter"/> has default value.
    /// </summary>
    public static class ResourceCollectionProvider
    {
        private static object lockObject= new object();
        private static Func<IResourceCollection> providerGetter = () => new ResourceCollectionBase();

        /// <summary>
        /// Function that returns instance of resource collection.
        /// Called only when initializing <see cref="ResourceTable.Resources"/>.
        /// </summary>
        public static Func<IResourceCollection> ProviderGetter
        {
            get { return providerGetter; }
            set
            {
                Guard.NotNull(value, "value");
                if (providerGetter != value)
                {
                    lock (lockObject)
                    {
                        providerGetter = value;
                    }
                }
            }
        }
    }
}
