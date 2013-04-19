using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap.Constraints
{
    public class AttributeConstraintProvider : IBootstrapConstraintProvider
    {
        private Func<Type, IBootstrapConstraint> factory;

        public AttributeConstraintProvider(Func<Type, IBootstrapConstraint> factory)
        {
            this.factory = factory;
        }

        public IEnumerable<IBootstrapConstraint> GetConstraints(Type bootstrapTask)
        {
            List<IBootstrapConstraint> result = new List<IBootstrapConstraint>();
            foreach (object attribute in bootstrapTask.GetCustomAttributes(true))
            {
                if (attribute is ConstraintAttribute)
                {
                    IBootstrapConstraint constraint = null;
                    if (attribute is IBootstrapConstraint)
                        constraint = (IBootstrapConstraint)attribute;
                    else
                        constraint = CreateInstance(((ConstraintAttribute)attribute).GetConstraintType());

                    if (constraint != null)
                        result.Add(constraint);
                }
            }
            return result;
        }

        protected IBootstrapConstraint CreateInstance(Type type)
        {
            return factory(type);
        }
    }
}
