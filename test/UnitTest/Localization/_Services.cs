using Neptuo.Activators;
using Neptuo.Localization;
using Neptuo.Localization.Readers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Localization
{
    class EmptyTranslationReader : ITranslationReader
    {
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

    class EmptyTranslationReaderFactory : IFactory<ITranslationReader, Stream>
    {
        public ITranslationReader Create(Stream context)
        {
            return new EmptyTranslationReader();
        }
    }

}
