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

            ILogSerializer serializer;
            if (formatter == null)
                serializer = new ConsoleSerializer();
            else
                serializer = new ConsoleSerializer(formatter, filter ?? new AllowedLogFilter());

            return logFactory.AddSerializer(serializer);
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

            ILogSerializer serializer;
            if (formatter == null)
                serializer = new TraceSerializer();
            else
                serializer = new TraceSerializer(formatter, filter ?? new AllowedLogFilter());

            return logFactory.AddSerializer(serializer);
        }
    }
}
