using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.StateMachines
{
    public class StateMachine<TItem, TState>
        where TState : class, IStateMachineState<TItem, TState>
    {
        protected TState InitialState { get; private set; }

        public event EventHandler<StateMachineEventArgs<TState>> OnEnterState;
        public event EventHandler<StateMachineEventArgs<TState>> OnLeaveState;

        public StateMachine(TState initialState)
        {
            if (initialState == null)
                throw new ArgumentNullException("initialState");

            InitialState = initialState;
        }

        public void OnEnterConcreteState<TConcreteState>(EventHandler<StateMachineEventArgs<TConcreteState>> handler)
            where TConcreteState : TState
        {
            if (handler == null)
                throw new ArgumentNullException("handler");

            OnEnterState += (sender, e) =>
            {
                if (e.State.GetType() == typeof(TConcreteState))
                    handler(this, new StateMachineEventArgs<TConcreteState>((TConcreteState)e.State));
            };
        }

        public void OnLeaveConcreteState<TConcreteState>(EventHandler<StateMachineEventArgs<TConcreteState>> handler)
            where TConcreteState : TState
        {
            if (handler == null)
                throw new ArgumentNullException("handler");

            OnLeaveState += (sender, e) =>
            {
                if (e.State.GetType() == typeof(TConcreteState))
                    handler(this, new StateMachineEventArgs<TConcreteState>((TConcreteState)e.State));
            };
        }

        public TState Process(IEnumerable<TItem> items)
        {
            if (items == null)
                throw new ArgumentNullException("items");

            TState currentState = InitialState;
            int index = 0;
            foreach (TItem item in items)
            {
                TState newState = currentState.Accept(item, index);
                if (newState == null)
                    throw new InvalidOperationException("StateMachine in invalid state, got null new state.");

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
