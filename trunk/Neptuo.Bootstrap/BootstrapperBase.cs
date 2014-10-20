using Neptuo.Bootstrap.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap
{
    public abstract class BootstrapperBase : IBootstrapper
    {
        private IBootstrapConstraintProvider provider;
        private Func<Type, IBootstrapTask> factory;

        protected List<IBootstrapTask> Tasks { get; private set; }

        public BootstrapperBase(Func<Type, IBootstrapTask> factory, IBootstrapConstraintProvider provider = null)
        {
            if (factory == null)
                throw new ArgumentNullException("factory");

            this.factory = factory;
            this.provider = provider ?? new NullObjectConstrainProvider();
            Tasks = new List<IBootstrapTask>();
        }

        protected IBootstrapTask CreateInstance(Type type)
        {
            return factory(type);
        }

        protected IBootstrapTask CreateInstance<T>()
            where T : IBootstrapTask
        {
            return factory(typeof(T));
        }

        protected bool SatisfiesConstraints(Type taskType)
        {
            IBootstrapConstraintContext context = new DefaultBootstrapConstraintContext(this);
            return provider.GetConstraints(taskType).Satisfies(context);
        }

        public virtual void Initialize()
        {
            IBootstrapConstraintContext context = new DefaultBootstrapConstraintContext(this);
            foreach (IBootstrapTask task in Tasks)
            {
                if (provider.GetConstraints(task.GetType()).Satisfies(context))
                    InitializeTask(task);
            }
        }

        protected virtual void InitializeTask(IBootstrapTask task)
        {
            task.Initialize();
        }
    }
}
