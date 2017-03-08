using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Validators.Messages
{
    /// <summary>
    /// Validation message for minimal and maximal string length.
    /// </summary>
    public class StringLengthMessage : ValidationMessageBase
    {
        /// <summary>
        /// Gets a minimal required string length.
        /// If <c>null</c> is returned, an lower bound is not set.
        /// </summary>
        public int? MinLength { get; private set; }

        /// <summary>
        /// Gets a maximal allowed string length.
        /// If <c>null</c> is returned, an upper bound is not set.
        /// </summary>
        public int? MaxLength { get; private set; }

        /// <summary>
        /// Creates a new instance for <paramref name="key"/>.
        /// </summary>
        /// <param name="key">A target field key.</param>
        /// <param name="minLength">Minimal required string length.</param>
        /// <param name="maxLength">Maximal allowed string length.</param>
        public StringLengthMessage(string key, int? minLength, int? maxLength)
            : base(key)
        {
            MinLength = minLength;
            MaxLength = maxLength;
        }

        public override string ToString()
        {
            if (MinLength == null)
                return String.Format("'{0}' must be shorter or equal to '{1}' characters.", Key, MaxLength);

            if (MaxLength == null)
                return String.Format("'{0}' must have length at least '{1}' characters.", Key, MinLength);

            return String.Format("Length of '{0}' must be between '{1}' and '{2}' characters.", MinLength, MaxLength);
        }
    }
}
