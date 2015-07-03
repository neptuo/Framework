using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Services.Validators.Messages
{
    /// <summary>
    /// Validation message with custom text message.
    /// </summary>
    public class TextValidationMessage : ValidationMessageBase
    {
        /// <summary>
        /// Creates new instance for <paramref name="key"/> with message in <paramref name="message"/>.
        /// </summary>
        /// <param name="key">Validation message key.</param>
        /// <param name="message">Text representation of validation message.</param>
        public TextValidationMessage(string key, string message)
            : base(key, message)
        { }
    }
}
