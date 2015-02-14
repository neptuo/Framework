using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Pipelines.Queries
{
    /// <summary>
    /// Dispatcher for queries.
    /// </summary>
    public interface IQueryDispatcher
    {
        /// <summary>
        /// Dispatches 
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<TOutput> Query<TOutput>(IQuery<TOutput> query);
    }
}
