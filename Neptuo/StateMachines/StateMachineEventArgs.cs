using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.StateMachines
{
    public class StateMachineEventArgs<TState>
    {
        public TState State { get; private set; }

        public StateMachineEventArgs(TState state)
        {
            State = state;
        }
    }
}
