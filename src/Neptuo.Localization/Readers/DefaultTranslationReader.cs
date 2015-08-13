using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Localization.Readers
{
    /// <summary>
    /// Implementation of <see cref="ITranslationReader"/> based on in-memory key-value collection.
    /// </summary>
    public class DefaultTranslationReader : ITranslationReader
    {
        private readonly Dictionary<string, string> values;

        /// <summary>
        /// Creates new empty instance.
        /// </summary>
        public DefaultTranslationReader()
        {
            values = new Dictionary<string, string>();
        }

        /// <summary>
        /// Creates new instance for <paramref name="culture"/> with initial translations of <paramref name="values"/>.
        /// </summary>
        /// <param name="values">Initial translations.</param>
        public DefaultTranslationReader(IDictionary<string, string> values)
        {
            Ensure.NotNull(values, "values");
            this.values = new Dictionary<string, string>(values);
        }

        /// <summary>
        /// Adds <paramref name="originalText"/> translation to <paramref name="translatedText"/>.
        /// </summary>
        /// <param name="originalText">Original text.</param>
        /// <param name="translatedText">Translated version of <paramref name="originalText"/>.</param>
        /// <returns>Self (for fluencyy).</returns>
        public DefaultTranslationReader Add(string originalText, string translatedText)
        {
            Ensure.NotNull(originalText, "originalText");
            values[originalText] = translatedText;
            return this;
        }

        public bool TryGet(string originalText, out string translatedText)
        {
            if(originalText == null)
            {
                translatedText = null;
                return false;
            }

            return values.TryGetValue(originalText, out translatedText);
        }
    }
}
