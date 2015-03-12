using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Globalization
{
    /// <summary>
    /// Methods for parsing culture info.
    /// </summary>
    public static class CultureInfoParser
    {
        /// <summary>
        /// Tries to parse <paramref name="value"/> into <see cref="CultureInfo"/>.
        /// </summary>
        /// <param name="value">Source text value.</param>
        /// <param name="cultureInfo">Target parsed culture info.</param>
        /// <returns>
        /// If <c>true</c>, parsing was successfull and <paramref name="cultureInfo"/> is set parsed culture info.
        /// otherwise <c>false</c> and <paramref name="cultureInfo"/> is set to <c>null</c>.
        /// </returns>
        public static bool TryParse(string value, out CultureInfo cultureInfo)
        {
            if (String.IsNullOrEmpty(value) || (value.Length != 5 && value.Length != 2))
            {
                cultureInfo = null;
                return false;
            }

            foreach (CultureInfo item in CultureInfo.GetCultures(CultureTypes.AllCultures))
            {
                if (
                    (value.Length == 5 && item.Name.ToLowerInvariant() == value.ToLowerInvariant())
                    ||
                    (value.Length == 2 && item.TwoLetterISOLanguageName.ToLowerInvariant() == value.ToLowerInvariant())
                ) 
                {
                    cultureInfo = item;
                    return true;
                }
            }

            cultureInfo = null;
            return false;
        }
    }
}
