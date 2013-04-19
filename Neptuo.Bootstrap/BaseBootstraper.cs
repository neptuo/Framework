using Neptuo.Bootstrap.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap
{
    public abstract class BaseBootstraper : IBootstrapper
    {
        private IBootstrapConstraintProvider provider;
        private Func<Type, IBootstrapTask> factory;

        protected List<IBootstrapTask> Tasks { get; private set; }

        public BaseBootstraper(Func<Type, IBootstrapTask> factory, IBootstrapConstraintProvider provider = null)
        {
            if (factory == null)
                throw new ArgumentNullException("factory");

            this.factory = factory;
            this.provider = provider;
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

        public virtual void Initialize()
        {
            foreach (IBootstrapTask task in Tasks)
            {
                if (provider == null || provider.GetConstraints(task.GetType()).Satisfies())
                    task.Initialize();
            }
        }
    }

    internal static class IEnumerableConstraintExtensions
    {
        public static bool Satisfies(this IEnumerable<IBootstrapConstraint> constraints)
        {
            foreach (IBootstrapConstraint constraint in constraints)
            {
                if (!constraint.Satisfies())
                    return false;
            }
            return true;
        }
    }
}
