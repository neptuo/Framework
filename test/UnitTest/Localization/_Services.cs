using Neptuo.Localization.GetText;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Localization
{
    class EmptyTranslationReader : ITranslationReader
    {
        public CultureInfo Culture { get; set; }

        public bool TryGet(string originalText, out string translatedText)
        {
            translatedText = null;
            return false;
        }
    }

    class DefaultCultureProvider : ICultureProvider
    {
        public IEnumerable<CultureInfo> GetCulture()
        {
            return new List<CultureInfo>() { new CultureInfo("cs") };
        }
    }

}
