using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Data.Entity.Queries
{
    internal class ParameterVisitor : ExpressionVisitor
    {
        private readonly ReadOnlyCollection<ParameterExpression> from;
        private readonly ReadOnlyCollection<ParameterExpression> to;

        public ParameterVisitor(ReadOnlyCollection<ParameterExpression> from, ReadOnlyCollection<ParameterExpression> to)
        {
            Ensure.NotNull(from, "from");
            Ensure.NotNull(to, "to");

            if (from.Count != to.Count)
                throw new InvalidOperationException("Parameter lengths must match.");

            this.from = from;
            this.to = to;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            for (int i = 0; i < from.Count; i++)
            {
                if (node == from[i])
                    return to[i];
            }

            return node;
        }
    }
}
