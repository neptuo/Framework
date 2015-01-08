using Neptuo.Data.Queries;
using Neptuo.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Data.Entity.Queries
{
    public static class EntityQuerySearch
    {
        internal static Expression BuildMultiValueSearch<TEntity, TNumber, TSearch>(Expression target, ParameterExpression parameter, Expression<Func<TEntity, TNumber>> getter, TSearch search)
            where TSearch : IMultiValueQuerySearch<TNumber>
        {
            if (search == null || search.Value.Count == 0)
                return target;

            Expression property = PropertyExpression(parameter, getter);
            ConstantExpression value = null;
            Expression compare = null;

            if (search.Value.Count == 1)
            {
                //MethodInfo equalsMethod = property.Type.GetMethod("Equals");
                value = Expression.Constant(search.Value[0]);
                compare = Expression.Equal(property, value);
            }
            else
            {
                value = Expression.Constant(search.Value);
                compare = Expression.Call(value, containsMethodName, Type.EmptyTypes, property);
            }

            return MergeExpression(target, compare);
        }

        public static Expression BuildIntSearch<TEntity>(Expression target, ParameterExpression parameter, Expression<Func<TEntity, int>> getter, IntSearch search)
        {
            return BuildMultiValueSearch(target, parameter, getter, search);
        }

        public static Expression BuildDoubleSearch<TEntity>(Expression target, ParameterExpression parameter, Expression<Func<TEntity, double>> getter, DoubleSearch search)
        {
            return BuildMultiValueSearch(target, parameter, getter, search);
        }

        public static Expression BuildDoubleSearch<TEntity>(Expression target, ParameterExpression parameter, Expression<Func<TEntity, decimal>> getter, DecimalSearch search)
        {
            return BuildMultiValueSearch(target, parameter, getter, search);
        }

        public static Expression BuildTextSearch<TEntity>(Expression target, ParameterExpression parameter, Expression<Func<TEntity, string>> getter, TextSearch search)
        {
            if (search == null || String.IsNullOrEmpty(search.Text))
                return target;

            Expression property = PropertyExpression(parameter, getter);
            ConstantExpression value = Expression.Constant(search.Text);

            Expression[] parameterValues = null;
            if (search.CaseSensitive)
                parameterValues = new Expression[] { value };
            else
                parameterValues = new Expression[] { value, Expression.Constant(StringComparison.OrdinalIgnoreCase) };

            Expression compare = null;
            switch (search.Type)
            {
                case TextSearchType.StartsWith:
                    compare = Expression.Call(property, TypeHelper.MethodName<string, string, bool>(s => s.StartsWith), Type.EmptyTypes, parameterValues);
                    break;
                case TextSearchType.EndsWith:
                    compare = Expression.Call(property, TypeHelper.MethodName<string, string, bool>(s => s.EndsWith), Type.EmptyTypes, parameterValues);
                    break;
                case TextSearchType.Contains:
                    if (search.CaseSensitive)
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

        public static Expression BuildDateTimeSearch<TEntity>(Expression target, ParameterExpression parameter, Expression<Func<TEntity, DateTime?>> getter, DateTimeSearch search)
        {
            if (search == null)
                return target;

            Expression property = PropertyExpression(parameter, getter);
            Expression value = null;
            Expression compare = null;

            switch (search.ComparePart)
            {
                case DateTimeSearchCompare.Date:
                    MethodInfo truncateTime = typeof(EntityFunctions).GetMethod("TruncateTime", new Type[] { typeof(DateTime?) });

                    value = Expression.Property(Expression.Call(truncateTime, Expression.Constant(search.DateTime, typeof(DateTime?))), "Value");
                    property = Expression.Property(Expression.Call(truncateTime, property), "Value");
                    break;
                case DateTimeSearchCompare.DateTime:
                    value = Expression.Constant(search.DateTime);
                    break;
            }

            Func<Expression, Expression, Expression> compareFunction = null;
            if (search.Type == (DateTimeSearchType.Before | DateTimeSearchType.Exactly))
                compareFunction = Expression.LessThanOrEqual;
            else if (search.Type == (DateTimeSearchType.Before | DateTimeSearchType.After))
                compareFunction = Expression.NotEqual;
            else if (search.Type == (DateTimeSearchType.After | DateTimeSearchType.Exactly))
                compareFunction = Expression.GreaterThanOrEqual;
            else if (search.Type == DateTimeSearchType.Before)
                compareFunction = Expression.LessThan;
            else if (search.Type == DateTimeSearchType.After)
                compareFunction = Expression.GreaterThan;
            else if (search.Type == DateTimeSearchType.Exactly)
                compareFunction = Expression.Equal;

            if (compareFunction == null)
                throw new NotSupportedException("Not supported date time compare combination.");

            compare = compareFunction(property, value);
            return MergeExpression(target, compare);
        }

        public static Expression BuildBoolSearch<TEntity>(Expression target, ParameterExpression parameter, Expression<Func<TEntity, bool?>> getter, BoolSearch search)
        {
            if (search == null)
                return target;

            Expression property = PropertyExpression(parameter, getter);
            Expression value = null;
            Expression compare = null;

            value = Expression.Constant(search.Value, typeof(bool?));

            if (search.IsNotNull)
                compare = Expression.NotEqual(property, value);
            else
                compare = Expression.Equal(property, value);

            return MergeExpression(target, compare);
        }

        #region Helper methods and fields

        private static readonly string containsMethodName = TypeHelper.MethodName<IEnumerable<int>, int, bool>(l => l.Contains);
        private static readonly string toDatePropertyName = TypeHelper.PropertyName<DateTime, DateTime>(d => d.Date);

        private static Expression PropertyExpression<TEntity, TPropertyType>(ParameterExpression parameter, Expression<Func<TEntity, TPropertyType>> getter)
        {
            ReadOnlyCollection<ParameterExpression> sourceParameters = getter.Parameters;
            ReadOnlyCollection<ParameterExpression> targetParameters = new ReadOnlyCollection<ParameterExpression>(new List<ParameterExpression> { parameter });

            ParameterVisitor visitor = new ParameterVisitor(sourceParameters, targetParameters);
            Expression propertyExpression = visitor.Visit(getter.Body);
            return propertyExpression;

            //Expression property = Expression.Property(parameter, TypeHelper.PropertyName<TEntity, TPropertyType>(getter));
            //return property;
        }

        private static Expression MergeExpression(Expression e1, Expression e2)
        {
            if (e1 == null)
                e1 = e2;
            else
                e1 = Expression.AndAlso(e1, e2);

            return e1;
        }

        #endregion
    }
}
