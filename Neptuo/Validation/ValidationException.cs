using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Validation
{
    public class ValidationException : Exception
    {
        public IValidationResult Result { get; private set; }

        public ValidationException(IValidationResult result)
        {
            Result = result;
        }
    }
}
