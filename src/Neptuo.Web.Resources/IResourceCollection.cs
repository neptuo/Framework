using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Resources
{
    /// <summary>
    /// Collection of resources.
    /// </summary>
    public interface IResourceCollection
    {
        /// <summary>
        /// Adds nes resource.
        /// Uses <see cref="IResource.Name"/> as key in collection.
        /// </summary>
        /// <param name="resource">New resource.</param>
        void Add(IResource resource);

        /// <summary>
        /// Tries to find resource with <paramref name="name"/>.
        /// Returns true if resource is found, than <paramref name="resource"/> is set.
        /// Otherwise returns false and <paramref name="resource"/> is set to null.
        /// </summary>
        /// <param name="name">Resource name.</param>
        /// <param name="resource">Instance of registered resource.</param>
        /// <returns>True/false whether contains resource with <paramref name="name"/>.</returns>
        bool TryGet(string name, out IResource resource);

        /// <summary>
        /// Returns enumeration of all registered resources.
        /// </summary>
        /// <returns>Enumeration of all registered resources.</returns>
        IEnumerable<IResource> EnumerateResources();

        /// <summary>
        /// Removes all registrations.
        /// </summary>
        void Clear();
    }
}
