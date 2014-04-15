using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Validation
{
    public class StringLengthMessage : PropertyValidationMessageBase
    {
        public int? MinLength { get; private set; }
        public int? MaxLength { get; private set; }

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
