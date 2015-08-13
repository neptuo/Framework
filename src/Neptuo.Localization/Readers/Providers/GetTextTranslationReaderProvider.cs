using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Localization.Readers.Providers
{
    /// <summary>
    /// GetText based implementation of <see cref="ITranslationReaderProvider"/> which for enumeration of default cultures
    /// doesn't call inner provider, but returns 'translatedText=sourceText' for any source text.
    /// </summary>
    public class GetTextTranslationReaderProvider : ITranslationReaderProvider
    {
        private readonly ICultureProvider defaultCultureProvider;
        private readonly ITranslationReaderProvider readerProvider;

        /// <summary>
        /// Creates new instance where <paramref name="defaultCultureProvider"/> provides enumeration of default cultures
        /// </summary>
        /// <param name="defaultCultureProvider">Provider of default (application) cultures.</param>
        /// <param name="readerProvider">Provider of readers for other cultures.</param>
        public GetTextTranslationReaderProvider(ICultureProvider defaultCultureProvider, ITranslationReaderProvider readerProvider)
        {
            Ensure.NotNull(defaultCultureProvider, "defaultCultureProvider");
            Ensure.NotNull(readerProvider, "readerProvider");
            this.defaultCultureProvider = defaultCultureProvider;
            this.readerProvider = readerProvider;
        }

        public bool TryGetReader(CultureInfo culture, Assembly assembly, out ITranslationReader reader)
        {
            IEnumerable<CultureInfo> defaultCultures = defaultCultureProvider.GetCulture();
            if(defaultCultures.Contains(culture))
            {
                reader = EmptyTranslationReader.SourceAsTranslated;
                return true;
            }

            return readerProvider.TryGetReader(culture, assembly, out reader);
        }
    }
}
