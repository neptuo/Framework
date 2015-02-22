using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Timers
{
    /// <summary>
    /// Base contract for invokable background running 
    /// </summary>
    public interface IBackgroundService
    {
        void Invoke();
    }
}
