using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Services.Validators.Messages
{
    /// <summary>
    /// Validation message for string not 'null or empty'.
    /// </summary>
    public class StringNullOrEmptyMessage : PropertyValidationMessageBase
    {
        /// <summary>
        /// Creates new instance for <paramref name="key"/>.
        /// </summary>
        /// <param name="key">Validation message key.</param>
        /// <param name="propertyName">Optional property name when differs from <paramref name="key"/>.</param>
        public StringNullOrEmptyMessage(string key, string propertyName = null)
            : base(key, propertyName)
        { }

        protected override string GetTextMessage()
        {
            return String.Format("{0} can't be empty.", PropertyName);
        }
    }
}
