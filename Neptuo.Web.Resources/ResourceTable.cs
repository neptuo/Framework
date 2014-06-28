using Neptuo.FileSystems;
using Neptuo.Web.Resources.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Resources
{
    /// <summary>
    /// Provides singleton instance of resource.
    /// </summary>
    public static class ResourceTable
    {
        /// <summary>
        /// Singleton instance of resources.
        /// </summary>
        public static IResourceCollection Resources { get; private set; }

        /// <summary>
        /// Loads to <see cref="ResourceTable.Resources"/> xml resources from <paramref name="file"/>.
        /// </summary>
        /// <param name="file">Xml file with resources.</param>
        public static void SetXmlCollection(IFile file)
        {
            Guard.NotNull(file, "file");
            Resources = new XmlResourceCollection(file);
        }
    }
}
