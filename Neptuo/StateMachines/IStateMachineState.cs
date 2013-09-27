using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.StateMachines
{
    public interface IStateMachineState<TItem, TState>
    {
        TState Accept(TItem item, int position);
    }
}
