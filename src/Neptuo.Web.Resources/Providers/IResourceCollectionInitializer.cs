using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Resources.Providers
{
    /// <summary>
    /// Provides methods for initializing collections of resources.
    /// </summary>
    public interface IResourceCollectionInitializer : IDisposable
    {
        /// <summary>
        /// Fills <paramref name="collection"/> with loaded data.
        /// </summary>
        /// <param name="collection">Collection to fill.</param>
        void FillCollection(IResourceCollection collection);
    }
}
