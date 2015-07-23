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
        /// Returns enumeration of <see cref="ITranslationReader"/> for <paramref name="cultures"/> and <paramref name="assembly"/>.
        /// </summary>
        /// <param name="cultures">Cultures to find readers for.</param>
        /// <param name="assembly">Assembly to find readers for.</param>
        /// <returns>Enumeration of <see cref="ITranslationReader"/> for <paramref name="cultures"/> and <paramref name="assembly"/>.</returns>
        IEnumerable<ITranslationReader> GetReaders(IEnumerable<CultureInfo> cultures, Assembly assembly);
    }
}
