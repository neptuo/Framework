using Neptuo.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Commands
{
    /// <summary>
    /// Main entry point to commanding infrastructure.
    /// </summary>
    public interface ICommandDispatcher
    {
        /// <summary>
        /// Posts <paramref name="command"/> to command infrastructure for execution.
        /// </summary>
        /// <param name="command">Instance describing requested operation.</param>
        void Handle(object command);
    }
}
