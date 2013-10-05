using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Linq.Expressions
{
    public abstract class CompiledExpression
    {
        internal abstract LambdaExpression BoxedGet { get; }
    }

    public sealed class CompiledExpression<T, TResult> : CompiledExpression
    {
        private Expression<Func<T, TResult>> expression;
        private Func<T, TResult> function;

        public CompiledExpression()
        { }

        public CompiledExpression(Expression<Func<T, TResult>> expression)
        {
            this.expression = expression;
            function = expression.Compile();
        }

        public TResult Evaluate(T instance)
        {
            return function(instance);
        }

        internal override LambdaExpression BoxedGet
        {
            get { return expression; }
        }
    }
}
