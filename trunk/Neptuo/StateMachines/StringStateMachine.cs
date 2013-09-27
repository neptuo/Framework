using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.StateMachines
{
    public class StringStateMachine<TState> : StateMachine<char, TState>
        where TState : class, IStringState<TState>
    {
        public StringStateMachine(TState initialState)
            : base(initialState)
        { }
    }
}
