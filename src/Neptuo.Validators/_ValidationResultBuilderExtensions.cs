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

        /// <summary>
        /// Adds <see cref="StringLengthMessage"/> to the <paramref name="builder" />
        /// </summary>
        /// <param name="builder">Result builder.</param>
        /// <param name="key">Validation message key.</param>
        /// <param name="minLength">Minimal required string length.</param>
        /// <param name="maxLength">Maximal allowed string length.</param>
        /// <returns><paramref name="builder"/>.</returns>
        public static IValidationResultBuilder AddStringLength(this IValidationResultBuilder builder, string key, int? minLength, int? maxLength)
        {
            Ensure.NotNull(builder, "builder");
            return builder.Add(new StringLengthMessage(key, minLength, maxLength));
        }

        /// <summary>
        /// Adds <see cref="StringNullOrEmptyMessage"/> to the <paramref name="builder" />
        /// </summary>
        /// <param name="builder">Result builder.</param>
        /// <param name="key">Validation message key.</param>
        /// <returns><paramref name="builder"/>.</returns>
        public static IValidationResultBuilder AddStringNullOrEmpty(this IValidationResultBuilder builder, string key)
        {
            Ensure.NotNull(builder, "builder");
            return builder.Add(new StringNullOrEmptyMessage(key));
        }

        /// <summary>
        /// Adds <see cref="KeyValuesMustMatchMessage"/> to the <paramref name="builder" />
        /// </summary>
        /// <param name="builder">Result builder.</param>
        /// <param name="key">Target field key.</param>
        /// <param name="otherKey">Other key that is required to be equal to the value of <paramref name="key"/>.</param>
        /// <returns><paramref name="builder"/>.</returns>
        public static IValidationResultBuilder AddKeyValuesMustMatch(this IValidationResultBuilder builder, string key, string otherKey)
        {
            Ensure.NotNull(builder, "builder");
            return builder.Add(new KeyValuesMustMatchMessage(key, otherKey));
        }
    }
}
