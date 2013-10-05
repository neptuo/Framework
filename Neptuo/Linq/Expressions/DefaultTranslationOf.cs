using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Linq.Expressions
{
    public static class DefaultTranslationOf<T>
    {
        public static CompiledExpression<T, TResult> Property<TResult>(Expression<Func<T, TResult>> property, Expression<Func<T, TResult>> expression)
        {
            return TranslationMap.defaultMap.Add(property, expression);
        }

        public static IncompletePropertyTranslation<TResult> Property<TResult>(Expression<Func<T, TResult>> property)
        {
            return new IncompletePropertyTranslation<TResult>(property);
        }

        public static TResult Evaluate<TResult>(T instance, MethodBase method)
        {
            var compiledExpression = TranslationMap.defaultMap.Get<T, TResult>(method);
            return compiledExpression.Evaluate(instance);
        }

        public class IncompletePropertyTranslation<TResult>
        {
            private Expression<Func<T, TResult>> property;

            internal IncompletePropertyTranslation(Expression<Func<T, TResult>> property)
            {
                this.property = property;
            }

            public CompiledExpression<T, TResult> Is(Expression<Func<T, TResult>> expression)
            {
                return DefaultTranslationOf<T>.Property(property, expression);
            }
        }
    }
}
