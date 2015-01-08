using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.StateMachines
{
    /// <summary>
    /// Implementation of state machine which works on enumeration of characters (string).
    /// </summary>
    /// <typeparam name="TState">Type of single state.</typeparam>
    public class StringStateMachine<TState> : StateMachine<char, TState>
        where TState : class, IStringState<TState>
    {
        /// <summary>
        /// Creates new instance with initial state <paramref name="initialState"/>.
        /// </summary>
        /// <param name="initialState">First state of state machine.</param>
        public StringStateMachine(TState initialState)
            : base(initialState)
        { }
    }
}
