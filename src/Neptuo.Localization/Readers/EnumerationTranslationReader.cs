using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Localization.Readers
{
    /// <summary>
    /// Implementation of <see cref="ITranslationReader"/> that reads from multiple readers.
    /// </summary>
    public class EnumerationTranslationReader : ITranslationReader
    {
        private readonly IEnumerable<ITranslationReader> readers;

        /// <summary>
        /// Creates new instance that reads from <paramref name="readers"/>.
        /// </summary>
        /// <param name="readers">Enumeration of readers to read from.</param>
        public EnumerationTranslationReader(IEnumerable<ITranslationReader> readers)
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
