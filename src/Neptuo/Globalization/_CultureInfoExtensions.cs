using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Globalization
{
    public static class _CultureInfoExtensions
    {
        /// <summary>
        /// Returns enumeration of parent cultures of <paramref name="cultureInfo"/> 
        /// (without self - <paramref name="cultureInfo"/> - and wihout <see cref="CultureInfo.InvariantCulture"/>).
        /// </summary>
        /// <param name="cultureInfo">Culture info to enumerate parents of.</param>
        /// <returns>Enumeration of parent cultures of <paramref name="cultureInfo"/>.</returns>
        public static IEnumerable<CultureInfo> Parents(this CultureInfo cultureInfo)
        {
            Ensure.NotNull(cultureInfo, "cultureInfo");

            while (cultureInfo.Parent != CultureInfo.InvariantCulture)
            {
                cultureInfo = cultureInfo.Parent;
                yield return cultureInfo;
            }
        }

        /// <summary>
        /// Returns enumeration of parent cultures of <paramref name="cultureInfo"/> 
        /// (with self - <paramref name="cultureInfo"/> - and wihout <see cref="CultureInfo.InvariantCulture"/>).
        /// </summary>
        /// <param name="cultureInfo">Culture info to enumerate parents of.</param>
        /// <returns>Enumeration of parent cultures of <paramref name="cultureInfo"/>.</returns>
        public static IEnumerable<CultureInfo> ParentsWithSelf(this CultureInfo cultureInfo)
        {
            Ensure.NotNull(cultureInfo, "cultureInfo");
            yield return cultureInfo;

            while (cultureInfo.Parent != CultureInfo.InvariantCulture)
            {
                cultureInfo = cultureInfo.Parent;
                yield return cultureInfo;
            }
        }
    }
}
