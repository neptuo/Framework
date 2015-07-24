using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Localization.GetText
{
    /// <summary>
    /// Implementation of <see cref="ITranslationReader"/> that reads from multiple readers.
    /// </summary>
    public class CompositeTranslationReader : ITranslationReader
    {
        private readonly IEnumerable<ITranslationReader> readers;

        public CultureInfo Culture
        {
            get
            {
                ITranslationReader firstReader = readers.FirstOrDefault();
                if (firstReader != null)
                    return firstReader.Culture;

                return CultureInfo.InvariantCulture;
            }
        }

        /// <summary>
        /// Creates new instance that reads from <paramref name="readers"/>.
        /// Culture info is provided from first item; if <paramref name="readers"/> is empty, culture is <see cref="CultureInfo.InvariantCulture"/>.
        /// </summary>
        /// <param name="readers">Enumeration of readers to read from.</param>
        public CompositeTranslationReader(IEnumerable<ITranslationReader> readers)
        {
            Ensure.NotNull(readers, "readers");
            this.readers = readers;
        }

        public bool TryGet(string originalText, out string translatedText)
        {
            foreach (ITranslationReader reader in readers)
            {
                if (reader.TryGet(originalText, out translatedText))
                    return true;
            }

            translatedText = null;
            return false;
        }
    }
}
