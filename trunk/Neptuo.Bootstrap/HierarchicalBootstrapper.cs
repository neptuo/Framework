using Neptuo.Bootstrap.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap
{
    /// <summary>
    /// TODO: Implement Hierarchical bootstrapper.
    /// </summary>
    public class HierarchicalBootstrapper : BootstrapperBase, IBootstrapper, IBootstrapTaskRegistry
    {
        public HierarchicalBootstrapper(Func<Type, IBootstrapTask> factory, IBootstrapConstraintProvider provider = null)
            : base(factory, provider)
        { }

        public void Register(IBootstrapTask task)
        {
            Tasks.Add(task);
        }

        public void Register<T>() 
            where T : IBootstrapTask
        {
            Tasks.Add(CreateInstance<T>());
        }
    }
}
