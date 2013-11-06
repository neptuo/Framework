using Neptuo.Commands.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Commands
{
    public interface ICommandDispatcher
    {
        void Handle<TCommand>(TCommand command);

        IValidationResult Validate<TCommand>(TCommand command);
    }
}
