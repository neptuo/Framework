using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Validators
{
    public interface IValidationDispatcher
    {
        IValidationResult Validate<TModel>(TModel model);
        IValidationResult Validate(object model);
    }
}
