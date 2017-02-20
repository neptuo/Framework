using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Neptuo.Observables.Commands
{
    /// <summary>
    /// A command which runs asynchronously and can be cancelled.
    /// </summary>
    public interface ICancellableCommand : ICommand
    {
        /// <summary>
        /// Stops execution of the command.
        /// If not running, nothing should happen.
        /// </summary>
        void Cancel();
    }
}
