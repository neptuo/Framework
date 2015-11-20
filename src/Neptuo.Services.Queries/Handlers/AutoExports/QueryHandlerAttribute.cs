using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Queries.Handlers.AutoExports
{
    /// <summary>
    /// Auto query handler wiring.
    /// Supports two scenarios:
    /// 1) Registers all implemented query handlers.
    ///    => Use parameterless constructor.
    /// 2) Registers only specified query handlers.
    ///    => Use constructor with one parameter.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class QueryHandlerAttribute : Attribute
    {
        /// <summary>
        /// Type of query to auto-wire (can be <c>null</c>).
        /// </summary>
        public Type QueryType { get; private set; }

        /// <summary>
        /// Whether is general use or type-specific usage.
        /// </summary>
        public bool HasTypeDefined { get; private set; }

        /// <summary>
        /// Register all implemented query handlers.
        /// </summary>
        public QueryHandlerAttribute()
        { }

        /// <summary>
        /// Registers only handler implementing <paramref name="queryType"/>.
        /// </summary>
        /// <param name="queryType">Type of query to auto-wire.</param>
        public QueryHandlerAttribute(Type queryType)
        {
            QueryType = queryType;
            HasTypeDefined = true;
        }
    }
}
