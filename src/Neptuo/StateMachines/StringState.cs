using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.StateMachines
{
    /// <summary>
    /// Base state of state machine which works on enumeration of characters (string).
    /// </summary>
    /// <typeparam name="TContext">Type of context which is automaticaly shared between state transitions.</typeparam>
    /// <typeparam name="TState">Type of targe state.</typeparam>
    public abstract class StringState<TContext, TState> : IStringState<TState>
        where TState : StringState<TContext, TState>
    {
        /// <summary>
        /// Placeholder for accumulating input when remaing in the same state.
        /// </summary>
        protected StringBuilder Text { get; set; }
        
        /// <summary>
        /// Automaticaly shared context object.
        /// </summary>
        protected TContext Context { get; set; }

        /// <summary>
        /// Creates new empty instance.
        /// </summary>
        protected StringState()
        {
            Text = new StringBuilder();
        }
        
        /// <summary>
        /// Creates instance of <typeparamref name="TNewState"/> as result of accepting input item.
        /// Automatically shares context object.
        /// </summary>
        /// <typeparam name="TNewState">Type of new state.</typeparam>
        /// <returns>Instance of new state with shared context objekt.</returns>
        protected virtual TNewState Move<TNewState>()
            where TNewState : TState, new()
        {
            TNewState newState = new TNewState();
            newState.Context = GetContextForNewState();
            return newState;
        }

        /// <summary>
        /// Factory method for context object used in <see cref="StringState{TContext,TState}.Move"/>
        /// </summary>
        /// <returns></returns>
        protected virtual TContext GetContextForNewState()
        {
            return Context;
        }

        public abstract TState Accept(char item, int position);
    }
}
