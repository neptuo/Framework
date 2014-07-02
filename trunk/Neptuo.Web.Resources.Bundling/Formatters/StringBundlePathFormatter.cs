using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Resources.Bundling.Formatters
{
    /// <summary>
    /// Formats bundle paths using two format strings with single argument.
    /// </summary>
    public class StringBundlePathFormatter : IBundlePathFormatter
    {
        private string javascriptFormat;
        private string stylesheetFormat;

        /// <summary>
        /// Creates new instance using <paramref name="javascriptFormat"/> for formatting javascript bundle paths
        /// and <paramref name="stylesheetFormat"/> for formatting stylesheet bundle paths.
        /// </summary>
        /// <param name="javascriptFormat">Format string with one argument for javascript bundle paths.</param>
        /// <param name="stylesheetFormat">Format string with one argument for stylesheet bundle paths.</param>
        public StringBundlePathFormatter(string javascriptFormat, string stylesheetFormat)
        {
            Guard.NotNullOrEmpty(javascriptFormat, "javascriptFormat");
            Guard.NotNullOrEmpty(stylesheetFormat, "stylesheetFormat");
            this.javascriptFormat = javascriptFormat;
            this.stylesheetFormat = stylesheetFormat;
        }

        public string FormatJavascriptPath(string resourceName)
        {
            return String.Format(javascriptFormat, resourceName);
        }

        public string FormatStylesheetPath(string resourceName)
        {
            return String.Format(stylesheetFormat, resourceName);
        }
    }
}
