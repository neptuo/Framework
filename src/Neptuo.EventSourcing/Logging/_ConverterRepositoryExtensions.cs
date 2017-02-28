using Neptuo;
using Neptuo.Converters;
using Neptuo.Logging.Serialization.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Logging
{
    /// <summary>
    /// Extensions for converters for EventSourcing logging.
    /// </summary>
    public static class _ConverterRepositoryExtensions
    {
        /// <summary>
        /// Adds converters for EventSourcing logging.
        /// </summary>
        /// <param name="repository">A converter repository.</param>
        /// <returns>A converter repository.</returns>
        public static IConverterRepository AddEventSourcingLogging(this IConverterRepository repository)
        {
            Ensure.NotNull(repository, "repository");
            return repository
                .Add(new EnvelopeMessageConverter());
        }
    }
}
