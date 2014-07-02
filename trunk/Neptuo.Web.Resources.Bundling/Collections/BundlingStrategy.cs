using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Resources.Bundling.Collections
{
    /// <summary>
    /// Defines strategies for creating bundles from resources.
    /// </summary>
    public enum BundlingStrategy
    {
        /// <summary>
        /// For each resource is created bundle.
        /// </summary>
        BundlePerResource,

        /// <summary>
        /// For each resource is created bundle including all resource dependencies.
        /// Dependency sources are included in 'this' bundle.
        /// </summary>
        BundlePerResourceWithDependencies
    }
}
