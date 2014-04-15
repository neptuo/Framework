using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Validation
{
    public class StringNullOrEmptyMessage : PropertyValidationMessageBase
    {
        public StringNullOrEmptyMessage(string key, string propertyName = null)
            : base(key, propertyName)
        { }

        protected override string GetTextMessage()
        {
            return String.Format("{0} can't be empty.", PropertyName);
        }
    }
}
