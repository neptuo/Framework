using Neptuo.Commands.Validation;
using Neptuo.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Commands
{
    public interface ICommandDispatcher
    {
        Task<TResult> Handle<TResult, TCommand>(TCommand command);

        IValidationResult Validate<TCommand>(TCommand command);
    }
}
