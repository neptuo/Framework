using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Resources
{
    /// <summary>
    /// Registered resource.
    /// </summary>
    public interface IResource
    {
        /// <summary>
        /// Resource name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Returns enumeration of registered javascript sources.
        /// </summary>
        /// <returns>Enumeration of registered javascript sources.</returns>
        IEnumerable<IJavascript> EnumerateJavascripts();

        /// <summary>
        /// Returns enumeration of registered stylesheet sources.
        /// </summary>
        /// <returns>Enumeration of registered stylesheet sources.</returns>
        IEnumerable<IStylesheet> EnumerateStylesheets();

        /// <summary>
        /// Returns enumeration of resource dependencies.
        /// </summary>
        /// <returns>Enumeration of resource dependencies.</returns>
        IEnumerable<IResource> EnumerateDependencies();
    }
}
