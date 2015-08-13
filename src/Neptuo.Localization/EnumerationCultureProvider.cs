using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Localization
{
    /// <summary>
    /// Implementation of <see cref="ICultureProvider"/> as static collection of cultures.
    /// </summary>
    public class EnumerationCultureProvider : ICultureProvider
    {
        private readonly IEnumerable<CultureInfo> cultures;

        /// <summary>
        /// Creates new instance that will contain <paramref name="cultures"/>.
        /// </summary>
        /// <param name="cultures">Array of cultures provided by this culture provider.</param>
        public EnumerationCultureProvider(params CultureInfo[] cultures)
        {
            Ensure.NotNull(cultures, "cultures");
            this.cultures = cultures;
        }

        /// <summary>
        /// Creates new instance that will contain <paramref name="cultures"/>.
        /// </summary>
        /// <param name="cultures">Enumeration of cultures provided by this culture provider.</param>
        public EnumerationCultureProvider(IEnumerable<CultureInfo> cultures)
        {
            Ensure.NotNull(cultures, "cultures");
            this.cultures = cultures;
        }

        public IEnumerable<CultureInfo> GetCulture()
        {
            return cultures;
        }
    }
}
