using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Localization
{
    /// <summary>
    /// Translates <paramref name="text"/> to the target language.
    /// </summary>
    /// <param name="text">Original text.</param>
    /// <returns><paramref name="text"/> translated to the target language.</returns>
    public delegate string TranslateHandler(string text);

    /// <summary>
    /// Translation helper.
    /// Enables to use translation handler (<see cref="TranslateHandler"/>) as singleton.
    /// </summary>
    public static class Translate
    {
        private static TranslateHandler handler = t => t;

        /// <summary>
        /// Sets <paramref name="handler"/> to be used for translations.
        /// If <paramref name="handler"/> is <c>null</c>, proxy/transient handler is used (output text is equal to the input text),
        /// </summary>
        /// <param name="handler"></param>
        public static void SetHandler(TranslateHandler handler)
        {
            if (handler == null)
                handler = t => t;

            Translate.handler = handler;
        }

        /// <summary>
        /// Translates <paramref name="text"/> to the target language.
        /// </summary>
        /// <param name="text">Original text.</param>
        /// <returns><paramref name="text"/> translated to the target language.</returns>
        public static string Text(string text)
        {
            return handler(text);
        }
    }
}
