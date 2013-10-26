using Neptuo.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.Validation
{
    public interface IModelValidationResult
    {
        bool IsValid { get; }
        IEnumerable<IValidationMessage> Messages { get; }
    }
}
