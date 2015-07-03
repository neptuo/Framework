using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Services.Validators.Messages
{
    /// <summary>
    /// Validation message based of property which is bound to.
    /// </summary>
    public class PropertyValidationMessageBase : ValidationMessageBase
    {
        private string propertyName;

        /// <summary>
        /// Validated property name.
        /// </summary>
        public string PropertyName
        {
            get { return propertyName ?? Key; }
        }

        /// <summary>
        /// Creates new instance with <paramref name="key"/> as validation message key 
        /// that is used as <see cref="PropertyValidationMessageBase.PropertyName"/> is <c>null</c>.
        /// </summary>
        /// <param name="key">Validation message key.</param>
        /// <param name="propertyName">Optional property name when differs from <paramref name="key"/>.</param>
        public PropertyValidationMessageBase(string key, string propertyName = null)
            : base(key)
        {
            this.propertyName = propertyName;
        }
    }
}
