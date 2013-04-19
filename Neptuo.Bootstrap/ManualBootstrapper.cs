using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap
{
    public class ManualBootstrapper : BaseBootstraper, IBootstrapper
    {
        public ManualBootstrapper(Func<Type, IBootstrapTask> factory)
            : base(factory)
        { }

        public void Register(Type type)
        {
            Tasks.Add(CreateInstance(type));
        }

        public void Register<T>()
            where T : IBootstrapTask
        {
            Register(typeof(T));
        }

        public void Register(IBootstrapTask task)
        {
            if (task != null)
                Tasks.Add(task);
        }
    }
}
