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
    /// Implementation of <see cref="ICultureProvider"/> that reads cultures from <see cref="Thread.CurrentThread"/>.
    /// </summary>
    public class CurrentThreadCultureProvider : ThreadCultureProviderBase
    {
        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="isUiCultureUsed">Whether to use UI culture of <see cref="ThreadCultureProviderBase.Thread"/></param>
        public CurrentThreadCultureProvider(bool isUiCultureUsed)
            : base(isUiCultureUsed)
        { }

        /// <summary>
        /// Returns <see cref="Thread.CurrentThread"/>.
        /// </summary>
        protected override Thread Thread
        {
            get { return Thread.CurrentThread; }
        }
    }
}
