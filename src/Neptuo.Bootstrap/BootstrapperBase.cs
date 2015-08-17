using Neptuo.Bootstrap.Constraints;
using Neptuo.Bootstrap.Constraints.Providers;
using Neptuo.Bootstrap.Handlers;
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
        private Func<Type, IBootstrapHandler> factory;

        protected List<IBootstrapHandler> Tasks { get; private set; }

        public BootstrapperBase(Func<Type, IBootstrapHandler> factory, IBootstrapConstraintProvider provider = null)
        {
            Ensure.NotNull(factory, "factory");
            this.factory = factory;
            this.provider = provider ?? new NullObjectConstrainProvider();
            Tasks = new List<IBootstrapHandler>();
        }

        protected IBootstrapHandler CreateInstance(Type type)
        {
            return factory(type);
        }

        protected IBootstrapHandler CreateInstance<T>()
            where T : IBootstrapHandler
        {
            return factory(typeof(T));
        }

        protected bool AreConstraintsSatisfied(IBootstrapHandler task)
        {
            IBootstrapConstraintContext context = new DefaultBootstrapConstraintContext(this);
            return provider.GetConstraints(task.GetType()).IsSatisfied(task, context);
        }

        public virtual void Initialize()
        {
            IBootstrapConstraintContext context = new DefaultBootstrapConstraintContext(this);
            foreach (IBootstrapHandler task in Tasks)
            {
                if (provider.GetConstraints(task.GetType()).IsSatisfied(task, context))
                    InitializeTask(task);
            }
        }

        protected virtual void InitializeTask(IBootstrapHandler task)
        {
            task.Initialize();
        }
    }
}
