using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Services.Validators.Messages
{
    /// <summary>
    /// Validation message for minimal and maximal string length.
    /// </summary>
    public class StringLengthMessage : PropertyValidationMessageBase
    {
        /// <summary>
        /// Minimal required string length.
        /// </summary>
        public int? MinLength { get; private set; }

        /// <summary>
        /// Maximal allowed string length.
        /// </summary>
        public int? MaxLength { get; private set; }

        /// <summary>
        /// Creates new instance for <paramref name="key"/>.
        /// </summary>
        /// <param name="key">Validation message key.</param>
        /// <param name="minLength">Minimal required string length.</param>
        /// <param name="maxLength">Maximal allowed string length.</param>
        /// <param name="propertyName">Optional property name when differs from <paramref name="key"/>.</param>
        public StringLengthMessage(string key, int? minLength, int? maxLength, string propertyName = null)
            : base(key, propertyName)
        {
            MinLength = minLength;
            MaxLength = maxLength;
        }

        protected override string GetTextMessage()
        {
            if (MinLength == null)
                return String.Format("{0} must be shorter or equal to {1} characters.", PropertyName, MaxLength);

            if (MaxLength == null)
                return String.Format("{0} must have length at least {1} characters.", PropertyName, MinLength);

            return String.Format("Length of {0} muset be between {1} and {2} characters.", MinLength, MaxLength);
        }
    }
}
