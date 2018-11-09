using Neptuo.Logging.Serialization;
using Neptuo.Logging.Serialization.Filters;
using Neptuo.Logging.Serialization.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Logging
{
    /// <summary>
    /// Serializers extensions for <see cref="ILogFactory"/>.
    /// </summary>
    public static class _LogFactoryExtensions
    {
        /// <summary>
        /// Adds <see cref="ConsoleSerializer"/> the to <paramref name="logFactory"/>.
        /// </summary>
        /// <param name="logFactory">Log factory to extend.</param>
        /// <param name="formatter">Optional formatter.</param>
        /// <param name="filter">Optional filter.</param>
        /// <returns><see cref="ILogFactory.AddSerializer"/>.</returns>
        public static ILogFactory AddConsole(this ILogFactory logFactory, ILogFormatter formatter = null, ILogFilter filter = null)
        {
            Ensure.NotNull(logFactory, "logFactory");

            if (filter == null)
                filter = AllowedLogFilter.Instance;

            if (formatter == null)
                formatter = DefaultLogFormatter.Instance;

            return logFactory.AddSerializer(new ConsoleSerializer(formatter, filter));
        }

        /// <summary>
        /// Adds <see cref="TraceSerializer"/> to the <paramref name="logFactory"/>.
        /// </summary>
        /// <param name="logFactory">Log factory to extend.</param>
        /// <param name="formatter">Optional formatter.</param>
        /// <param name="filter">Optional filter.</param>
        /// <returns><see cref="ILogFactory.AddSerializer"/>.</returns>
        public static ILogFactory AddTrace(this ILogFactory logFactory, ILogFormatter formatter = null, ILogFilter filter = null)
        {
            Ensure.NotNull(logFactory, "logFactory");

            if (filter == null)
                filter = AllowedLogFilter.Instance;

            if (formatter == null)
                formatter = DefaultLogFormatter.Instance;

            return logFactory.AddSerializer(new TraceSerializer(formatter, filter));
        }
    }
}
