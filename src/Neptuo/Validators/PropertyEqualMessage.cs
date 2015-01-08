using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Validators
{
    /// <summary>
    /// Validation message for comparing two properties.
    /// </summary>
    public class PropertyEqualMessage : PropertyValidationMessageBase
    {
        public string OtherProperty { get; private set; }

        public PropertyEqualMessage(string key, string otherProperty, string propertyName = null)
            : base(key, propertyName)
        {
            OtherProperty = otherProperty;
        }

        protected override string GetTextMessage()
        {
            return String.Format("{0} must match {1}.", PropertyName, OtherProperty);
        }
    }
}
