using Neptuo.Data.Queries;
using Neptuo.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Data.Entity.Queries
{
    public static class EntityQuerySearch
    {
        public static Expression BuildIntSearch<TEntity>(Expression target, Expression parameter, Expression<Func<TEntity, int>> getter, IntSearch intSearch)
        {
            if (intSearch == null || intSearch.Value.Count == 0)
                return target;

            MemberExpression property = PropertyExpression(parameter, getter);
            ConstantExpression value = null;
            Expression compare = null;

            if (intSearch.Value.Count == 1)
            {
                value = Expression.Constant(intSearch.Value[0]);
                compare = Expression.Equal(property, value);
            }
            else
            {
                value = Expression.Constant(intSearch.Value);
                compare = Expression.Call(value, TypeHelper.MethodName<List<int>, int, bool>(l => l.Contains), Type.EmptyTypes, property);
            }

            return MergeExpression(target, compare);
        }

        public static Expression BuildTextSearch<TEntity>(Expression target, Expression parameter, Expression<Func<TEntity, string>> getter, TextSearch textSearch)
        {
            if (textSearch == null || String.IsNullOrEmpty(textSearch.Text))
                return target;

            MemberExpression property = PropertyExpression(parameter, getter);
            ConstantExpression value = Expression.Constant(textSearch.Text);

            Expression[] parameterValues = null;
            if (textSearch.CaseSensitive)
                parameterValues = new Expression[] { value };
            else
                parameterValues = new Expression[] { value, Expression.Constant(StringComparison.OrdinalIgnoreCase) };

            Expression compare = null;
            switch (textSearch.Type)
            {
                case TextSearchType.StartsWith:
                    compare = Expression.Call(property, TypeHelper.MethodName<string, string, bool>(s => s.StartsWith), Type.EmptyTypes, parameterValues);
                    break;
                case TextSearchType.EndsWith:
                    compare = Expression.Call(property, TypeHelper.MethodName<string, string, bool>(s => s.EndsWith), Type.EmptyTypes, parameterValues);
                    break;
                case TextSearchType.Contains:
                    if (textSearch.CaseSensitive)
                        compare = Expression.Call(property, TypeHelper.MethodName<string, string, bool>(s => s.Contains), Type.EmptyTypes, value);
                    else
                        throw new NotImplementedException();
                    break;
                case TextSearchType.Match:
                    compare = Expression.Equal(property, value);
                    break;
                default:
                    throw new DataException("Invalid value for 'TextSearch.Type'.");
            }

            return MergeExpression(target, compare);
        }


        private static MemberExpression PropertyExpression<TEntity, TPropertyType>(Expression parameter, Expression<Func<TEntity, TPropertyType>> getter)
        {
            return Expression.Property(parameter, TypeHelper.PropertyName<TEntity, TPropertyType>(getter));
        }

        private static Expression MergeExpression(Expression e1, Expression e2)
        {
            if (e1 == null)
                e1 = e2;
            else
                e1 = Expression.AndAlso(e1, e2);

            return e1;
        }
    }
}
