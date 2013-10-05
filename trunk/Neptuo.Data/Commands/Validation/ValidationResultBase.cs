using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Data.Commands.Validation
{
    public class ValidationResultBase : IValidationResult
    {
        public bool IsValid { get; set; }

        public ValidationResultBase(bool isValid)
        {
            IsValid = isValid;
        }
    }
}
