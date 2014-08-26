using Neptuo.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Validators
{
    public class ValidationResultBase : IValidationResult
    {
        public bool IsValid { get; set; }
        public IEnumerable<IValidationMessage> Messages { get; set; }

        public ValidationResultBase(bool isValid)
            : this(isValid, new List<IValidationMessage>())
        { }

        public ValidationResultBase(bool isValid, IEnumerable<IValidationMessage> messages)
        {
            if (messages == null)
                throw new ArgumentNullException("messages");

            IsValid = isValid;
            Messages = messages;
        }
    }
}
