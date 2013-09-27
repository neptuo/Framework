using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.StateMachines
{
    public abstract class StringState<TContext, TState> : IStringState<TState>
        where TState : StringState<TContext, TState>
    {
        protected StringBuilder Text { get; set; }
        protected TContext Context { get; set; }

        public StringState()
        {
            Text = new StringBuilder();
        }

        protected virtual TNewState Move<TNewState>()
            where TNewState : TState, new()
        {
            TNewState newState = new TNewState();
            newState.Context = GetContextForNewState();
            return newState;
        }

        protected virtual TContext GetContextForNewState()
        {
            return Context;
        }

        public abstract TState Accept(char item, int position);
    }
}
