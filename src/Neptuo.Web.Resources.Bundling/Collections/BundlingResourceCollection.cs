using Neptuo.Web.Resources.Bundling.Formatters;
using Neptuo.Web.Resources.FileResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Optimization;

namespace Neptuo.Web.Resources.Bundling.Collections
{
    /// <summary>
    /// Wrapps inner resource collection and creates bundles for registered resources.
    /// </summary>
    /// <remarks>
    /// ISSUES:
    /// 1) Removes all meta data from source resources.
    /// </remarks>
    public class BundlingResourceCollection : IResourceCollection
    {
        private BundleCollection bundleCollection;
        private IResourceCollection innerCollection;
        private IBundlePathFormatter formatter;
        private BundlingStrategy strategy;

        /// <summary>
        /// Creates new instance using <paramref name="innerCollection"/> as resource target collection.
        /// </summary>
        /// <param name="innerCollection">Bundle resources registered to.</param>
        /// <param name="formatter">Used for converting resource names to bundle virtual paths.</param>
        /// <param name="strategy">Strategy for including dependencies.</param>
        public BundlingResourceCollection(IResourceCollection innerCollection, IBundlePathFormatter formatter, BundlingStrategy strategy)
        {
            Guard.NotNull(innerCollection, "innerCollection");
            Guard.NotNull(formatter, "formatter");
            this.bundleCollection = BundleTable.Bundles;
            this.innerCollection = innerCollection;
            this.formatter = formatter;
            this.strategy = strategy;
        }

        /// <summary>
        /// Requires <paramref name="resource"/> to be fully prepared.
        /// Any items added to <paramref name="resource"/> after calling this method will be ignored.
        /// </summary>
        /// <param name="resource">Resource to create and register bundle from.</param>
        public void Add(IResource resource)
        {
            Guard.NotNull(resource, "resource");

            FileResource innerResource = new FileResource(resource.Name);
            if (ProcessResourceContent(resource, innerResource))
                innerCollection.Add(innerResource);
        }

        /// <summary>
        /// Goes through resource dependecy graph and creates javascript and stylesheet bundles.
        /// Returns <c>true</c> if javascript or stylesheet bundle was created; false otherwise.
        /// </summary>
        /// <param name="resource">Resource to go through.</param>
        /// <param name="innerResource">Javascript and stylesheet bundle target.</param>
        /// <returns><c>true</c> if javascript or stylesheet bundle was created; false otherwise.</returns>
        private bool ProcessResourceContent(IResource resource, FileResource innerResource)
        {
            // Create bundle for javascript files.
            bool containsJavascript = false;
            Bundle javascriptBundle = new Bundle(formatter.FormatJavascriptPath(resource.Name));

            // Create bundle for stylesheet files.
            bool containsStylesheet = false;
            Bundle stylesheetBundle = new Bundle(formatter.FormatStylesheetPath(resource.Name));

            // Find whole dependency graph and enumerate.
            foreach (IResource dependency in FindResources(resource))
            {
                // Add each javascript file to javascript bundle.
                foreach (IJavascript javascript in dependency.EnumerateJavascripts())
                {
                    javascriptBundle.Include(javascript.Source);
                    containsJavascript = true;
                }

                // Add each stylesheet file to stylesheet bundle.
                foreach (IStylesheet stylesheet in dependency.EnumerateStylesheets())
                {
                    stylesheetBundle.Include(stylesheet.Source);
                    containsStylesheet = true;
                }
            }

            // If javascript bundle is not empty, register it.
            if (containsJavascript)
            {
                bundleCollection.Add(javascriptBundle);
                innerResource.AddJavascript(new FileJavascript(javascriptBundle.Path));
            }

            // If stylesheet bundle is not empty, register it.
            if (containsStylesheet)
            {
                bundleCollection.Add(stylesheetBundle);
                innerResource.AddStylesheet(new FileStylesheet(stylesheetBundle.Path));
            }

            return containsJavascript || containsStylesheet;
        }

        /// <summary>
        /// Creates dependency graph including <paramref name="resource"/>.
        /// 
        /// </summary>
        /// <param name="resource"></param>
        /// <returns></returns>
        private IEnumerable<IResource> FindResources(IResource resource)
        {
            IEnumerable<IResource> result = Enumerable.Empty<IResource>();
            if (strategy == BundlingStrategy.BundlePerResourceWithDependencies)
            {
                foreach (IResource dependency in resource.EnumerateDependencies())
                    result = Enumerable.Union(result, FindResources(dependency));
            }
            result = Enumerable.Union(result, Enumerable.Repeat(resource, 1));
            return result;
        }

        public bool TryGet(string name, out IResource resource)
        {
            return innerCollection.TryGet(name, out resource);
        }

        public IEnumerable<IResource> EnumerateResources()
        {
            return innerCollection.EnumerateResources();
        }

        public void Clear()
        {
            innerCollection.Clear();
        }
    }
}
