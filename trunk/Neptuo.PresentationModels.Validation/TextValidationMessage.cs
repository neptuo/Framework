using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.Validation
{
    public class TextValidationMessage : IValidationMessage
    {
        public string FieldIdentifier { get; private set; }
        public string Message { get; private set; }

        public TextValidationMessage(string fieldIdentifier, string message)
        {
            if (message == null)
                throw new ArgumentNullException("message");

            FieldIdentifier = fieldIdentifier;
            Message = message;
        }
    }
}
