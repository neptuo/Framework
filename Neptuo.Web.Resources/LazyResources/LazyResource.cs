using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Resources.LazyResources
{
    /// <summary>
    /// Lazy implementation of <see cref="IResource"/> that finds real resource only when needed.
    /// </summary>
    public class LazyResource : IResource
    {
        private IResourceCollection collection;
        private IResource innerResource;

        public string Name { get; private set; }

        /// <summary>
        /// Creates new instance with <paramref name="resourceName"/> and <paramref name="collection"/> as source for finding real resource.
        /// </summary>
        /// <param name="collection">Source for finding real resource.</param>
        /// <param name="resourceName">Resource nane.</param>
        public LazyResource(IResourceCollection collection, string resourceName)
        {
            Guard.NotNull(collection, "collection");
            Guard.NotNullOrEmpty(resourceName, "resourceName");
            this.collection = collection;
            Name = resourceName;
        }

        private void EnsureResource()
        {
            if (innerResource == null && !collection.TryGet(Name, out innerResource))
                throw new InvalidOperationException(String.Format("Unnable to find resource with name '{0}'.", Name));
        }

        public string Meta(string key, string defaultValue)
        {
            EnsureResource();
            return innerResource.Meta(key, defaultValue);
        }

        public IEnumerable<IJavascript> EnumerateJavascripts()
        {
            EnsureResource();
            return innerResource.EnumerateJavascripts();
        }

        public IEnumerable<IStylesheet> EnumerateStylesheets()
        {
            EnsureResource();
            return innerResource.EnumerateStylesheets();
        }

        public IEnumerable<IResource> EnumerateDependencies()
        {
            EnsureResource();
            return innerResource.EnumerateDependencies();
        }
    }
}
