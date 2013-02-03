using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap
{
    public class AutomaticBootstraper : BaseBootstraper, IBootstraper
    {
        public AutomaticBootstraper(Func<Type, IBootstrapTask> factory)
            : base(factory)
        { }

        public override void Initialize()
        {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {
                    foreach (Type type in assembly.GetTypes())
                    {
                        if (typeof(IBootstrapTask).IsAssignableFrom(type))
                            Tasks.Add(type);
                    }
                }
                catch (Exception) { }
            }

            base.Initialize();
        }
    }
}
