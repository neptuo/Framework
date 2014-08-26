using Neptuo.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Validators
{
    public interface IValidationResult
    {
        bool IsValid { get; }
        IEnumerable<IValidationMessage> Messages { get; }
    }
}
