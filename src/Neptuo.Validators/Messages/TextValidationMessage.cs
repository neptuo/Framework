using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Validators.Messages
{
    /// <summary>
    /// Validation message with custom text content.
    /// </summary>
    public class TextValidationMessage : ValidationMessageBase
    {
        private readonly string message;

        /// <summary>
        /// Creates a new instance for <paramref name="key"/> with message in <paramref name="message"/>.
        /// </summary>
        /// <param name="key">A target field key.</param>
        /// <param name="message">Text representation of validation message.</param>
        public TextValidationMessage(string key, string message)
            : base(key)
        {
            this.message = message;
        }

        public override string ToString()
        {
            return message;
        }
    }
}
