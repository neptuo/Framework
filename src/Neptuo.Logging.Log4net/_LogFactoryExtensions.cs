using Neptuo.Logging.Serialization;
using Neptuo.Logging.Serialization.Formatters;
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
        /// <param name="formatter">Optional formatter.</param>
        /// <returns><see cref="ILogFactory.AddSerializer"/>.</returns>
        public static ILogFactory AddLog4net(this ILogFactory logFactory, ILogFormatter formatter = null)
        {
            Ensure.NotNull(logFactory, "logFactory");

            ILogSerializer serializer;
            if (formatter == null)
                serializer = new Log4netSerializer();
            else
                serializer = new Log4netSerializer(formatter);

            return logFactory.AddSerializer(serializer);
        }
    }
}
