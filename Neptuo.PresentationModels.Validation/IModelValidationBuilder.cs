using Neptuo.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.Validation
{
    public interface IModelValidationBuilder
    {
        void AddMessage(IValidationMessage message);
        void AddMessages(IEnumerable<IValidationMessage> messages);
        IValidationResult ToResult();
    }
}
