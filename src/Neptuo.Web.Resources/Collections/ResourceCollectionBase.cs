using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Resources.Collections
{
    /// <summary>
    /// Base implementation for <see cref="IResourceCollection"/>.
    /// </summary>
    public class ResourceCollectionBase : IResourceCollection
    {
        /// <summary>
        /// Inner storage for implementations.
        /// </summary>
        private Dictionary<string, IResource> resources;

        /// <summary>
        /// Creates new instance.
        /// </summary>
        public ResourceCollectionBase()
        {
            resources = new Dictionary<string, IResource>();
        }

        /// <summary>
        /// Adds nes resource.
        /// Uses <see cref="IResource.Name"/> as key in collection.
        /// Requires <paramref name="resource"/> to be not null and <paramref name="resource.Name"/> to be not null or empty.
        /// </summary>
        /// <param name="resource">New resource.</param>
        public void Add(IResource resource)
        {
            Guard.NotNull(resource, "resource");
            Guard.NotNullOrEmpty(resource.Name, "resource.Name");
            resources[resource.Name] = resource;
        }

        /// <summary>
        /// Tries to find resource with <paramref name="name"/>.
        /// Returns true if resource is found, than <paramref name="resource"/> is set.
        /// Otherwise returns false and <paramref name="resource"/> is set to null.
        /// Requires <paramref name="name"/> to be not null.
        /// </summary>
        /// <param name="name">Resource name.</param>
        /// <param name="resource">Instance of registered resource.</param>
        /// <returns>True/false whether contains resource with <paramref name="name"/>.</returns>
        public bool TryGet(string name, out IResource resource)
        {
            Guard.NotNullOrEmpty(name, "name");
            return resources.TryGetValue(name, out resource);
        }

        /// <summary>
        /// Returns enumeration of all registered resources.
        /// </summary>
        /// <returns>Enumeration of all registered resources.</returns>
        public IEnumerable<IResource> EnumerateResources()
        {
            return resources.Values;
        }

        /// <summary>
        /// Removes all registrations.
        /// </summary>
        public void Clear()
        {
            resources.Clear();
        }
    }
}
