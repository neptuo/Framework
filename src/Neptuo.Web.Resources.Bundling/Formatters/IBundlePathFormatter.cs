using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Resources.Bundling.Formatters
{
    /// <summary>
    /// Converts resource names to virtual bundle paths.
    /// </summary>
    public interface IBundlePathFormatter
    {
        /// <summary>
        /// Converts <paramref name="resourceName"/> to bundle virtual path for javascript file.
        /// </summary>
        /// <param name="resourceName">Registered resource name.</param>
        /// <returns>Bundle virtual path for javascript file.</returns>
        string FormatJavascriptPath(string resourceName);

        /// <summary>
        /// Converts <paramref name="resourceName"/> to bundle virtual path for stylesheet file.
        /// </summary>
        /// <param name="resourceName">Registered resource name.</param>
        /// <returns>Bundle virtual path for stylesheet file.</returns>
        string FormatStylesheetPath(string resourceName);
    }
}
