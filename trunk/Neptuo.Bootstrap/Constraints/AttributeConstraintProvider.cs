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
                    if (attribute is IBootstrapConstraint)
                        result.Add((IBootstrapConstraint)attribute);
                    else
                        result.Add(CreateInstance(((ConstraintAttribute)attribute).GetConstraintType()));
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
