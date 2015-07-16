using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Reflections.Enumerators
{
    /// <summary>
    /// Contract for defining type filters and handlers.
    /// </summary>
    /// <typeparam name="TContext">Type of context information.</typeparam>
    public interface ITypeDeletegateCollection<TContext>
    {
        /// <summary>
        /// Adds filters. All filters and conjucted.
        /// </summary>
        /// <param name="filter">Delegate, that returns <c>true</c> to pass type handler and <c>false</c> to filter out passed type.</param>
        /// <returns>Self (for fluency).</returns>
        ITypeDeletegateCollection<TContext> AddFilter(Func<Type, TContext, bool> filter);

        /// <summary>
        /// Adds handler, that will be execute for every type, that matches all filters.
        /// </summary>
        /// <param name="handler">Delegate to be executed for every type, that matches all filters.</param>
        /// <returns>Self (for fluency).</returns>
        ITypeDeletegateCollection<TContext> AddHandler(Action<Type, TContext> handler);
    }

    /// <summary>
    /// Contract for defining type filters and handlers.
    /// </summary>
    public interface ITypeDeletegateCollection
    {
        /// <summary>
        /// Adds filters. All filters and conjucted.
        /// </summary>
        /// <param name="filter">Delegate, that returns <c>true</c> to pass type handler and <c>false</c> to filter out passed type.</param>
        /// <returns>Self (for fluency).</returns>
        ITypeDeletegateCollection AddFilter(Func<Type, bool> filter);

        /// <summary>
        /// Adds handler, that will be execute for every type, that matches all filters.
        /// </summary>
        /// <param name="handler">Delegate to be executed for every type, that matches all filters.</param>
        /// <returns>Self (for fluency).</returns>
        ITypeDeletegateCollection AddHandler(Action<Type> handler);
    }
}
