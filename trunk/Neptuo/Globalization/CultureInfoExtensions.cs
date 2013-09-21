using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Globalization
{
    public static class CultureInfoExtensions
    {
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
