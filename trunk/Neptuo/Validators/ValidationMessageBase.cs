using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Validators
{
    /// <summary>
    /// Base implementation of <see cref="IValidationMessage"/>.
    /// </summary>
    public class ValidationMessageBase : IValidationMessage
    {
        private string message;

        public string Key { get; private set; }

        public string Message
        {
            get
            {
                if (message == null)
                    message = GetTextMessage();

                return message;
            }
            set { message = value; }
        }

        /// <summary>
        /// Creates new instance for <paramref name="key"/> with message in <paramref name="message"/>.
        /// </summary>
        /// <param name="key">Validation message key.</param>
        /// <param name="message">Text representation of validation message.</param>
        public ValidationMessageBase(string key, string message = null)
        {
            Key = key;
            Message = message;
        }

        /// <summary>
        /// Formats text representation of validation message.
        /// Method is called only when message was not passed in ctor.
        /// Should be overriden in derivered classes when message parameter in ctor was not used.
        /// </summary>
        /// <returns>Text representation of validation message.</returns>
        protected virtual string GetTextMessage()
        {
            return String.Empty;
        }
    }
}
