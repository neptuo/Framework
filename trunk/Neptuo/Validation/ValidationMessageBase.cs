using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Validation
{
    public class ValidationMessageBase : IValidationMessage
    {
        private string message;

        public string Key { get; private set; }
        public string Message
        {
            get
            {
                if (message == null)
                    message = GetTextMessage();

                return message;
            }
            set { message = value; }
        }

        public ValidationMessageBase(string key, string message = null)
        {
            Key = key;
            Message = message;
        }

        protected virtual string GetTextMessage()
        {
            return String.Empty;
        }
    }
}
