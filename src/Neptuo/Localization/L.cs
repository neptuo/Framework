using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Localization
{/// <summary>
    /// Localization helper.
    /// </summary>
    /// <example>
    /// string localized = (L)"String to localize";
    /// </example>
    public struct L
    {
        /// <summary>
        /// Text to localize
        /// </summary>
        string text;

        public static string T(string s)
        {
            return s;
        }

        public static implicit operator L(string s)
        {
            return new L() { text = s };
            //return new L(s);
        }

        public static implicit operator string(L l)
        {
            return T(l.ToString());
        }

        public override string ToString()
        {
            return LocalizationService.Current[text];
        }
    }   
}
