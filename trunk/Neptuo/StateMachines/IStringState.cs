using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.StateMachines
{
    public interface IStringState<TState> : IStateMachineState<char, TState> 
    { }
}
