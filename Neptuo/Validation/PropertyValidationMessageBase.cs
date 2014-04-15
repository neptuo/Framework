using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Validation
{
    public class PropertyValidationMessageBase : ValidationMessageBase
    {
        private string propertyName;

        public string PropertyName
        {
            get { return propertyName ?? Key; }
        }

        public PropertyValidationMessageBase(string key, string propertyName = null)
            : base(key)
        {
            this.propertyName = propertyName;
        }
    }
}
