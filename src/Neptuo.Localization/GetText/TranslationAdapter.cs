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
    /// Adapter between <see cref="Neptuo.Localization.Translate"/> and GetText implementation of localizations.
    /// </summary>
    public class TranslationAdapter
    {
        private readonly ICultureProvider cultureProvider;
        private readonly ITranslationReader reader;

        /// <summary>
        /// Creates new instance that reads culture from <paramref name="cultureProvider"/>.
        /// </summary>
        /// <param name="cultureProvider">Provider for user culture.</param>
        /// <param name="reader">Translations provider.</param>
        public TranslationAdapter(ICultureProvider cultureProvider, ITranslationReader reader)
        {
            Ensure.NotNull(cultureProvider, "cultureProvider");
            Ensure.NotNull(reader, "reader");
            this.cultureProvider = cultureProvider;
            this.reader = reader;
        }

        public string Translate(Assembly callingAssembly, string originalText)
        {
            IEnumerable<CultureInfo> cultureInfos = cultureProvider.GetCulture();
            foreach (CultureInfo cultureInfo in cultureInfos)
            {
                string translatedText;
                if (reader.TryGet(cultureInfo, originalText, out translatedText))
                    return translatedText;
            }

            return originalText;
        }
    }
}
