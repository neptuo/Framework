using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Validation
{
    public class TextValidationMessage : IValidationMessage
    {
        public string Key { get; private set; }
        public string Message { get; private set; }

        public TextValidationMessage(string key, string message)
        {
            if (message == null)
                throw new ArgumentNullException("message");

            Key = key;
            Message = message;
        }
    }
}
