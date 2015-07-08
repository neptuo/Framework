using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Services.Queries.Routing
{
    /// <summary>
    /// Default (with manual registration) implementation of <see cref="IRouteTable"/>.
    /// </summary>
    public class DefaultRouteTable : IRouteTable
    {
        private readonly Dictionary<Type, string> storage = new Dictionary<Type, string>();

        /// <summary>
        /// Add mapping for <paramref name="queryType"/> to <paramref name="url"/>.
        /// </summary>
        /// <param name="queryType">Type of query object.</param>
        /// <param name="url">Url to be routed to.</param>
        /// <returns>Self (for fluency).</returns>
        public DefaultRouteTable Add(Type queryType, string url)
        {
            Ensure.NotNull(queryType, "queryType");
            Ensure.NotNull(url, "url");
            storage[queryType] = url;
            return this;
        }

        public bool TryGet(Type queryType, out string url)
        {
            Ensure.NotNull(queryType, "queryType");
            return storage.TryGetValue(queryType, out url);
        }
    }
}
