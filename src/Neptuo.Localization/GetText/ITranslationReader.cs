using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Localization.GetText
{
    /// <summary>
    /// Reads translations from source.
    /// </summary>
    public interface ITranslationReader
    {
        /// <summary>
        /// Tries to find translation of <paramref name="originalText"/>.
        /// </summary>
        /// <param name="originalText">Original text to find translation of.</param>
        /// <param name="translatedText">Translated <paramref name="originalText"/>.</param>
        /// <returns><c>true</c> when contains translation of <paramref name="originalText"/>; <c>false</c> otherwise.</returns>
        bool TryGet(string originalText, out string translatedText);
    }
}