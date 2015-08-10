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
        private readonly ITranslationReaderProvider readerProvider;

        /// <summary>
        /// Creates new instance that reads culture from <paramref name="cultureProvider"/>.
        /// </summary>
        /// <param name="cultureProvider">Provider for user culture.</param>
        /// <param name="readerProvider">Translations provider.</param>
        public TranslationAdapter(ICultureProvider cultureProvider, ITranslationReaderProvider readerProvider)
        {
            Ensure.NotNull(cultureProvider, "cultureProvider");
            Ensure.NotNull(readerProvider, "readerProvider");
            this.cultureProvider = cultureProvider;
            this.readerProvider = readerProvider;
        }

        public string Translate(Assembly callingAssembly, string originalText)
        {
            IEnumerable<CultureInfo> cultureInfos = cultureProvider.GetCulture();
            foreach (CultureInfo cultureInfo in cultureInfos)
            {
                ITranslationReader reader;
                string translatedText;
                if (readerProvider.TryGetReader(cultureInfo, callingAssembly, out reader) && reader.TryGet(originalText, out translatedText))
                    return translatedText;
            }

            return originalText;
        }
    }
}
