using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap.Dependencies
{
    public interface ITaskExecutor
    {
        void Execute(IBootstrapTask task);
    }

    public interface ITaskDescriptor
    {
        Type Type { get; }
        IBootstrapTask Instance { get; }
        bool IsExecuted { get; }
    }
}
