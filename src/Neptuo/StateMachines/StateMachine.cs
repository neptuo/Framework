using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.StateMachines
{
    /// <summary>
    /// Implementation of state machine which works on enumeration of <typeparamref name="TItem"/> and supports states of type <typeparamref name="TState"/>.
    /// </summary>
    /// <typeparam name="TItem">Type of item (whole input is enumeration of this type).</typeparam>
    /// <typeparam name="TState">Type of single state.</typeparam>
    public class StateMachine<TItem, TState>
        where TState : class, IStateMachineState<TItem, TState>
    {
        /// <summary>
        /// First state of state machine.
        /// </summary>
        protected TState InitialState { get; private set; }

        /// <summary>
        /// Event fired when entering (before accepting input item) state.
        /// </summary>
        public event EventHandler<StateMachineEventArgs<TState>> OnEnterState;

        /// <summary>
        /// Event fired when leaving (after accepting input item) state.
        /// </summary>
        public event EventHandler<StateMachineEventArgs<TState>> OnLeaveState;

        /// <summary>
        /// Creates new instance with initial state <paramref name="initialState"/>.
        /// </summary>
        /// <param name="initialState">First state of state machine.</param>
        public StateMachine(TState initialState)
        {
            Ensure.NotNull(initialState, "initialState");
            InitialState = initialState;
        }

        /// <summary>
        /// Adds <paramref name="handler"/> to listening on event <see cref="StateMachine{TContext,TState}.OnEnterState"/> if entered state is of type <typeparamref name="TConcreteState"/>.
        /// </summary>
        /// <param name="handler">Handler for processing event.</param>
        public void OnEnterConcreteState<TConcreteState>(EventHandler<StateMachineEventArgs<TConcreteState>> handler)
            where TConcreteState : TState
        {
            Ensure.NotNull(handler, "handler");
            OnEnterState += (sender, e) =>
            {
                if (e.State.GetType() == typeof(TConcreteState))
                    handler(this, new StateMachineEventArgs<TConcreteState>((TConcreteState)e.State));
            };
        }

        /// <summary>
        /// Adds <paramref name="handler"/> to listening on event <see cref="StateMachine{TContext,TState}.OnLeaveState"/> if left state is of type <typeparamref name="TConcreteState"/>.
        /// </summary>
        /// <param name="handler">Handler for processing event.</param>
        public void OnLeaveConcreteState<TConcreteState>(EventHandler<StateMachineEventArgs<TConcreteState>> handler)
            where TConcreteState : TState
        {
            Ensure.NotNull(handler, "handler");
            OnLeaveState += (sender, e) =>
            {
                if (e.State.GetType() == typeof(TConcreteState))
                    handler(this, new StateMachineEventArgs<TConcreteState>((TConcreteState)e.State));
            };
        }

        /// <summary>
        /// Processes <paramref name="items"/> to returns state in which state machine remains after processing whole input.
        /// </summary>
        /// <param name="items">Enumeration of input.</param>
        /// <returns>State, in which state machine remains after processing whole input.</returns>
        public TState Process(IEnumerable<TItem> items)
        {
            Ensure.NotNull(items, "items");
            TState currentState = InitialState;
            int index = 0;
            foreach (TItem item in items)
            {
                TState newState = currentState.Accept(item, index);
                if (newState == null)
                    throw Ensure.Exception.InvalidOperation("StateMachine in invalid state, got null new state.");

                if (newState != currentState)
                {
                    if (OnLeaveState != null)
                        OnLeaveState(this, new StateMachineEventArgs<TState>(currentState));

                    if (OnEnterState != null)
                        OnEnterState(this, new StateMachineEventArgs<TState>(newState));
                }
                currentState = newState;
                index++;
            }
            return currentState;
        }
    }
}
