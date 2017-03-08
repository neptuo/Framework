using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Validators.Messages
{
    /// <summary>
    /// A base implementation of <see cref="IValidationMessage"/>.
    /// </summary>
    public abstract class ValidationMessageBase : IValidationMessage
    {
        /// <summary>
        /// Gets a target field key.
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// Creates a new instance for <paramref name="key"/>.
        /// </summary>
        /// <param name="key">A target field key.</param>
        public ValidationMessageBase(string key)
        {
            Key = key;
        }
    }
}
