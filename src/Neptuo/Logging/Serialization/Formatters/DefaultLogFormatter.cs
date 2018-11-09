using Neptuo.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Logging.Serialization.Formatters
{
    /// <summary>
    /// Default implementation of <see cref="ILogFormatter"/>.
    /// Tries to convert model using <see cref="Converts"/>.
    /// </summary>
    public class DefaultLogFormatter : ILogFormatter
    {
        /// <summary>
        /// Gets a singleton.
        /// </summary>
        public static DefaultLogFormatter Instance { get; } = new DefaultLogFormatter();

        private readonly IConverterRepository converts;

        /// <summary>
        /// Creates a new instances.
        /// </summary>
        public DefaultLogFormatter()
            : this(Converts.Repository)
        { }

        /// <summary>
        /// Creates a new instance with <paramref name="converts"/> used to convert messages to string.
        /// </summary>
        /// <param name="converts">A repository of converters.</param>
        public DefaultLogFormatter(IConverterRepository converts)
        {
            Ensure.NotNull(converts, "converts");
            this.converts = converts;
        }

        public string Format(string scopeName, LogLevel level, object model)
        {
            object message;
            if (!converts.TryConvert(model.GetType(), typeof(string), model, out message))
                message = model;

            return String.Format(
                "{0} {1}({2}){3}{3}{4}{3}",
                DateTime.Now,
                scopeName,
                level.ToString().ToUpperInvariant(),
                Environment.NewLine,
                message
            );
        }
    }
}
