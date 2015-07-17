using Neptuo.Logging.Serialization;
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
        /// <returns><see cref="ILogFactory.AddSerializer"/>.</returns>
        public static ILogFactory AddConsole(this ILogFactory logFactory)
        {
            Ensure.NotNull(logFactory, "logFactory");
            return logFactory.AddSerializer(new ConsoleSerializer());
        }

        /// <summary>
        /// Adds <see cref="TraceSerializer"/> to the <paramref name="logFactory"/>.
        /// </summary>
        /// <param name="logFactory">Log factory to extend.</param>
        /// <returns><see cref="ILogFactory.AddSerializer"/>.</returns>
        public static ILogFactory AddTrace(this ILogFactory logFactory)
        {
            Ensure.NotNull(logFactory, "logFactory");
            return logFactory.AddSerializer(new TraceSerializer());
        }
    }
}
