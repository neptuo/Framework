using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Linq.Expressions
{
    internal class TranslatingExpressionVisitor : ExpressionVisitor
    {
        private readonly Stack<KeyValuePair<ParameterExpression, Expression>> bindings = new Stack<KeyValuePair<ParameterExpression, Expression>>();
        private readonly TranslationMap map;

        internal TranslatingExpressionVisitor(TranslationMap map)
        {
            this.map = map;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            EnsureTypeInitialized(node.Member.DeclaringType);

            CompiledExpression cp;
            if (map.TryGetValue(node.Member, out cp))
                return VisitCompiledExpression(cp, node.Expression);

            if (typeof(CompiledExpression).IsAssignableFrom(node.Member.DeclaringType))
                return VisitCompiledExpression(cp, node.Expression);

            return base.VisitMember(node);
        }

        private Expression VisitCompiledExpression(CompiledExpression ce, Expression expression)
        {
            bindings.Push(new KeyValuePair<ParameterExpression, Expression>(ce.BoxedGet.Parameters.Single(), expression));
            var body = Visit(ce.BoxedGet.Body);
            bindings.Pop();
            return body;
        }

        protected override Expression VisitParameter(ParameterExpression p)
        {
            var binding = bindings.Where(b => b.Key == p).FirstOrDefault();
            return (binding.Value == null) ? base.VisitParameter(p) : Visit(binding.Value);
        }
        
        private static void EnsureTypeInitialized(Type type)
        {
            try
            {
                // Ensure the static members are accessed class' ctor
                RuntimeHelpers.RunClassConstructor(type.TypeHandle);
            }
            catch (TypeInitializationException)
            {

            }
        }
    }
}
