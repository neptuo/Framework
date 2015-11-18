using Neptuo.Validators.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Validators
{
    /// <summary>
    /// Common extensions for <see cref="IValidationResultBuilder"/>.
    /// </summary>
    public static class _ValidationResultBuilderExtensions
    {
        /// <summary>
        /// Adds <see cref="TextValidationMessage"/> to the <paramref name="builder"/>.
        /// </summary>
        /// <param name="builder">Result builder.</param>
        /// <param name="key">Message key.</param>
        /// <param name="message">Text format.</param>
        /// <param name="parameters">Optional parameters for <paramref name="message"/>.</param>
        /// <returns><paramref name="builder"/>.</returns>
        public static IValidationResultBuilder AddTextMessage(this IValidationResultBuilder builder, string key, string message, params object[] parameters)
        {
            Ensure.NotNull(builder, "builder");
            return builder.Add(new TextValidationMessage(key, String.Format(message, parameters)));
        }
    }
}
