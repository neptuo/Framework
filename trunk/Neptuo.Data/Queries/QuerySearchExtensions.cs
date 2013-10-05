using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Data.Queries
{
    public static class QuerySearchExtensions
    {
        public static IQuery<TResult, TFilter> WhereInt<TResult, TFilter>(
            this IQuery<TResult, TFilter> query, Expression<Func<TFilter, IntSearch>> selector, params int[] values)
        {
            return query.Where(selector, IntSearch.Create(values));
        }

        public static IQuery<TResult, TFilter> WhereText<TResult, TFilter>(
            this IQuery<TResult, TFilter> query, Expression<Func<TFilter, TextSearch>> selector, string text, TextSearchType type = TextSearchType.Match, bool caseSensitive = true)
        {
            return query.Where(selector, TextSearch.Create(text, type, caseSensitive));
        }
    }
}
