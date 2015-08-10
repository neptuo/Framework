using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Services.Validators.Messages
{
    /// <summary>
    /// Base implementation of <see cref="IValidationMessage"/>.
    /// </summary>
    public abstract class ValidationMessageBase : IValidationMessage
    {
        private readonly string message;
        public string Key { get; private set; }

        /// <summary>
        /// Creates new instance for <paramref name="key"/> with message in <paramref name="message"/>.
        /// </summary>
        /// <param name="key">Validation message key.</param>
        public ValidationMessageBase(string key)
        {
            Key = key;
        }

        /// <summary>
        /// Creates new instance for <paramref name="key"/> with message in <paramref name="message"/>.
        /// </summary>
        /// <param name="key">Validation message key.</param>
        /// <param name="message">Text representation of validation message.</param>
        public ValidationMessageBase(string key, string message)
            : this(key)
        {
            this.message = message;
        }

        public override string ToString()
        {
            return message;
        }
    }
}
