using Neptuo.Globalization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Neptuo.Localization
{
    /// <summary>
    /// Base implementation of <see cref="ICultureProvider"/> for reading culture from <see cref="System.Threading.Thread"/>.
    /// Provides all current culture with all parents (except for <see cref="CultureInfo.InvariantCulture"/>).
    /// </summary>
    public abstract class ThreadCultureProviderBase : ICultureProvider
    {
        /// <summary>
        /// Whether to use UI culture of <see cref="ThreadCultureProviderBase.Thread"/>
        /// </summary>
        protected bool IsUiCultureUsed { get; private set; }

        /// <summary>
        /// Thread to read culture from.
        /// </summary>
        protected abstract Thread Thread { get; }

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="isUiCultureUsed">Whether to use UI culture of <see cref="ThreadCultureProviderBase.Thread"/></param>
        protected ThreadCultureProviderBase(bool isUiCultureUsed)
        {
            IsUiCultureUsed = isUiCultureUsed;
        }

        /// <summary>
        /// Returns enumeration of <see cref="CultureInfo"/> from <see cref="ThreadCultureProviderBase.Thread"/>.
        /// Provides all current culture with all parents (except for <see cref="CultureInfo.InvariantCulture"/>).
        /// </summary>
        /// <returns>Enumeration of <see cref="CultureInfo"/> from <see cref="ThreadCultureProviderBase.Thread"/>.</returns>
        public IEnumerable<CultureInfo> GetCulture()
        {
            CultureInfo cultureInfo = IsUiCultureUsed 
                ? Thread.CurrentUICulture 
                : Thread.CurrentCulture;

            return cultureInfo.ParentsWithSelf();
        }
    }
}
