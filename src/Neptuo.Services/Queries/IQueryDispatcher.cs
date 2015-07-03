using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Services.Queries
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
        Task<TOutput> QueryAsync<TOutput>(IQuery<TOutput> query);
    }
}
