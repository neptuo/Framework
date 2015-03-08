using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.StateMachines
{
    /// <summary>
    /// Describes context of <see cref="StateMachine{TContext,TState}.OnEnterState"/> or <see cref="StateMachine{TContext,TState}.OnLeaveState" /> event.
    /// </summary>
    /// <typeparam name="TState">Type of target state.</typeparam>
    public class StateMachineEventArgs<TState>
    {
        /// <summary>
        /// Current state of state machine.
        /// </summary>
        public TState State { get; private set; }

        /// <summary>
        /// Creates new instance with <paramref name="state"/> as current state machine state.
        /// </summary>
        /// <param name="state">Current state of state machine.</param>
        public StateMachineEventArgs(TState state)
        {
            Ensure.NotNull(state, "state");
            State = state;
        }
    }
}
