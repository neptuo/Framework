using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap
{
    public class ManualBootstraper : BaseBootstraper, IBootstraper
    {
        public ManualBootstraper(Func<Type, IBootstrapTask> factory)
            : base(factory)
        { }

        public void Register(Type type)
        {
            Tasks.Add(type);
        }

        public void Register<T>()
            where T : IBootstrapTask
        {
            Register(typeof(T));
        }
    }
}
