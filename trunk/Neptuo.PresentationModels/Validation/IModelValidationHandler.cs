using Neptuo.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.Validators
{
    /// <summary>
    /// Defines presentation model validator.
    /// </summary>
    public interface IModelValidationHandler : IValidationHandler<IModelValueGetter>
    {
    }
}
