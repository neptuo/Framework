using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.StateMachines
{
    /// <summary>
    /// Represents single of state machine.
    /// </summary>
    /// <typeparam name="TItem">State machine item type (whole input is enumeration of this type).</typeparam>
    /// <typeparam name="TState">Type of target state.</typeparam>
    public interface IStateMachineState<TItem, TState>
    {
        /// <summary>
        /// Processes logic on input <paramref name="item"/> and returns new (or this) state of state machine to move into.
        /// </summary>
        /// <param name="item">Current input element.</param>
        /// <param name="position">Position (index) in whole input.</param>
        /// <returns>New (or this) state of state machine to move into.</returns>
        TState Accept(TItem item, int position);
    }
}
