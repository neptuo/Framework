using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo
{
    public interface ISchedulingContext
    {
        Envelope Envelope { get; }

        void Execute();
    }
}
