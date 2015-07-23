using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Localization
{
    /// <summary>
    /// Translation helper.
    /// </summary>
    public static class Translate
    {
        private static Func<Assembly, string, string> defaultHandler = (a, t) => t;
        private static Func<Assembly, string, string> handler = defaultHandler;

        /// <summary>
        /// Sets <paramref name="handler"/> to be used for translations.
        /// If <paramref name="handler"/> is <c>null</c>, proxy/transient handler is used (output text is equal to the input text),
        /// </summary>
        /// <param name="handler">Translation handler.</param>
        public static void SetHandler(Func<Assembly, string, string> handler)
        {
            if (handler == null)
                handler = (a, t) => t;

            Translate.handler = handler;
        }

        /// <summary>
        /// Sets <paramref name="handler"/> to be used for translations.
        /// If <paramref name="handler"/> is <c>null</c>, proxy/transient handler is used (output text is equal to the input text),
        /// </summary>
        /// <param name="handler">Translation handler.</param>
        public static void SetHandler(Func<string, string> handler)
        {
            if (handler == null)
                handler = t => t;

            Translate.handler = (a, t) => handler(t);
        }

        /// <summary>
        /// Translates <paramref name="text"/> to the target language.
        /// </summary>
        /// <param name="text">Original text.</param>
        /// <returns><paramref name="text"/> translated to the target language.</returns>
        public static string Text(string text)
        {
            return Text(Assembly.GetCallingAssembly(), text);
        }

        internal static string Text(Assembly assembly, string text)
        {
            return handler(assembly, text);
        }
    }
}
