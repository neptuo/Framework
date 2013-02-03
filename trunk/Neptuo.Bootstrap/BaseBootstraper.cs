using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap
{
    public abstract class BaseBootstraper : IBootstraper
    {
        private Func<Type, IBootstrapTask> factory;

        protected List<Type> Tasks { get; private set; }

        public BaseBootstraper(Func<Type, IBootstrapTask> factory)
        {
            this.factory = factory;

            Tasks = new List<Type>();
        }

        public virtual void Initialize()
        {
            if (factory == null)
                throw new ArgumentNullException("factory");

            List<IBootstrapTask> instances = new List<IBootstrapTask>();
            foreach (Type type in Tasks)
            {
                IBootstrapTask task = factory(type);
                if (task != null)
                    instances.Add(task);
            }

            foreach (IBootstrapTask task in instances)
                task.Initialize();
        }
    }
}
