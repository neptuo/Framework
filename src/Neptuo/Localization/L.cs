using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Localization
{
    /// <summary>
    /// Localization facade.
    /// Internally calls <see cref="Translate.Text"/> for translation.
    /// </summary>
    /// <example>
    /// string localized = (L)"String to localize";
    /// </example>
    public struct L
    {
        private readonly string originalText;
        private readonly string translatedText;

        /// <summary>
        /// Text to translate.
        /// </summary>
        public string OriginalText { get { return originalText; } }

        /// <summary>
        /// <see cref="L.OriginalText"/> after translation.
        /// </summary>
        public string TranslatedText { get { return translatedText; } }

        /// <summary>
        /// Private constructor to create instance and translate <paramref name="text"/>.
        /// </summary>
        /// <param name="assembly">Calling assembly.</param>
        /// <param name="text">Text to translate.</param>
        private L(Assembly assembly, string text)
        {
            originalText = text;
            translatedText = Translate.Text(assembly, text);
        }

        /// <summary>
        /// Explicit creation from <see cref="string"/> value of <paramref name="text"/>.
        /// </summary>
        /// <param name="text">Original text.</param>
        /// <returns>Instance of translation structure for <paramref name="text"/>.</returns>
        public static explicit operator L(string text)
        {
            return new L(Assembly.GetCallingAssembly(), text);
        }

        /// <summary>
        /// Implicit serialization to <see cref="string"/>.
        /// </summary>
        /// <param name="instance">Instance to be serialized to string.</param>
        /// <returns>Value of <see cref="L.ToString"/>.</returns>
        public static implicit operator string(L instance)
        {
            Ensure.NotNull(instance, "instance");
            return instance.ToString();
        }

        /// <summary>
        /// To string implementation using translated text.
        /// </summary>
        /// <returns>Translated text.</returns>
        public override string ToString()
        {
            return TranslatedText;
        }
    }
}
