using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Logging
{
    /// <summary>
    /// Writer extensions for <see cref="ILogFactory"/>.
    /// </summary>
    public static class _LogFactoryExtensions
    {
        /// <summary>
        /// Adds <see cref="ConsoleLogWriter"/> to <paramref name="logFactory"/>.
        /// </summary>
        /// <param name="logFactory">Log factory to extend.</param>
        /// <returns><see cref="ILogFactory.AddWriter"/>.</returns>
        public static ILogFactory AddConsoleWriter(this ILogFactory logFactory)
        {
            Ensure.NotNull(logFactory, "logFactory");
            return logFactory.AddWriter(new ConsoleLogWriter());
        }
    }
}
