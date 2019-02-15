using Neptuo.Net.Http.Routing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Neptuo.Queries
{
    /// <summary>
    /// An implementation of <see cref="IQueryDispatcher"/> that transfers queries over HTTP.
    /// </summary>
    public class HttpQueryDispatcher : IQueryDispatcher
    {
        private readonly ObjectSender objectSender;

        /// <summary>
        /// Creates a new instance which sends objects using <paramref name="objectSender"/>.
        /// </summary>
        /// <param name="objectSender">An object sender.</param>
        public HttpQueryDispatcher(ObjectSender objectSender)
        {
            Ensure.NotNull(objectSender, "objectSender");
            this.objectSender = objectSender;
        }

        public async Task<TResult> QueryAsync<TQuery, TResult>(TQuery query, CancellationToken cancellationToken = default)
            where TQuery : IQuery<TResult>
        {
            Ensure.NotNull(query, "query");
            return (TResult) await objectSender.SendAsync(query, cancellationToken);
        }
    }
}
