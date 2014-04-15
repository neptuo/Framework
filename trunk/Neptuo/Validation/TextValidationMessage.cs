using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Validation
{
    public class TextValidationMessage : ValidationMessageBase
    {
        public string Key { get; private set; }
        public string Message { get; private set; }

        public TextValidationMessage(string key, string message)
            : base(key, message)
        { }
    }
}
