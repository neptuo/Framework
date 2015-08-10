using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Localization
{
    /// <summary>
    /// Provides for user cultures.
    /// Cultures should be sort by preference.
    /// </summary>
    public interface ICultureProvider
    {
        /// <summary>
        /// Returns sorted enumeration of user prefered cultures.
        /// </summary>
        /// <returns>Sorted enumeration of user prefered cultures.</returns>
        IEnumerable<CultureInfo> GetCulture();
    }
}
