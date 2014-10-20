using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap.Constraints
{
    internal static class IEnumerableConstraintExtensions
    {
        public static bool Satisfies(this IEnumerable<IBootstrapConstraint> constraints, IBootstrapConstraintContext context)
        {
            foreach (IBootstrapConstraint constraint in constraints)
            {
                if (!constraint.Satisfies(context))
                    return false;
            }

            return true;
        }
    }
}
