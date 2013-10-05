using Neptuo.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Linq
{
    public static class ExpressiveExtensions
    {
        public static IQueryable<T> WithTranslations<T>(this IQueryable<T> source)
        {
            return source.Provider.CreateQuery<T>(WithTranslations(source.Expression));
        }

        public static IQueryable<T> WithTranslations<T>(this IQueryable<T> source, TranslationMap map)
        {
            return source.Provider.CreateQuery<T>(WithTranslations(source.Expression, map));
        }

        public static Expression WithTranslations(Expression expression)
        {
            return WithTranslations(expression, TranslationMap.defaultMap);
        }

        public static Expression WithTranslations(Expression expression, TranslationMap map)
        {
            return new TranslatingExpressionVisitor(map).Visit(expression);
        }
    }
}
