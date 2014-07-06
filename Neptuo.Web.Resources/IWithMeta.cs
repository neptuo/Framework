using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Resources
{
    /// <summary>
    /// Base resource with collection of meta values.
    /// </summary>
    public interface IWithMeta
    {
        /// <summary>
        /// Gets meta data value associated with <paramref name="key"/>.
        /// If such meta data is not found, returns <paramref name="defaultValue"/>.
        /// </summary>
        /// <param name="key">Key to meta data collection. Can't be null or empty.</param>
        /// <param name="defaultValue">Default value if such key is not found.</param>
        /// <returns>Meta data associated with <paramref name="key"/> or <paramref name="defaultValue"/>.</returns>
        string Meta(string key, string defaultValue);
    }
}
