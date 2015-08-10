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
    /// Provides <see cref="ITranslationReader"/> based on culture and assembly.
    /// </summary>
    public interface ITranslationReaderProvider
    {
        /// <summary>
        /// Tries to get <paramref name="reader" /> for <paramref name="culture"/> and <paramref name="assembly"/>.
        /// </summary>
        /// <param name="culture">Cultures to find readers for.</param>
        /// <param name="assembly">Assembly to find readers for.</param>
        /// <return><c>true</c> when there is reader; <c>false</c> otherwise.</returns>
        bool TryGetReader(CultureInfo culture, Assembly assembly, out ITranslationReader reader);
    }
}
