using Neptuo.Logging.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Logging
{
    /// <summary>
    /// Log4net extensions of <see cref="ILogFactory"/>.
    /// </summary>
    public static class _LogFactoryExtensions
    {
        /// <summary>
        /// Adds <see cref="Log4netSerializer"/> the to <paramref name="logFactory"/>.
        /// </summary>
        /// <param name="logFactory">Log factory to extend.</param>
        /// <returns><see cref="ILogFactory.AddSerializer"/>.</returns>
        public static ILogFactory AddLog4net(this ILogFactory logFactory)
        {
            Ensure.NotNull(logFactory, "logFactory");
            return logFactory.AddSerializer(new Log4netSerializer());
        }
    }
}
