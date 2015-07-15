using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Reflections.Enumerators
{
    /// <summary>
    /// Contract for enumerating types.
    /// </summary>
    /// <typeparam name="TContext">Type of context information.</typeparam>
    public interface ITypeEnumerator<TContext>
    {
        /// <summary>
        /// Adds filters. All filters and conjucted.
        /// </summary>
        /// <param name="filter">Delegate, that returns <c>true</c> to pass type handler and <c>false</c> to filter out passed type.</param>
        /// <returns>Self (for fluency).</returns>
        ITypeEnumerator<TContext> AddFilter(Func<Type, TContext, bool> filter);

        /// <summary>
        /// Adds handler, that will be execute for every type, that matches all filters.
        /// </summary>
        /// <param name="handler">Delegate to be executed for every type, that matches all filters.</param>
        /// <returns>Self (for fluency).</returns>
        ITypeEnumerator<TContext> AddHandler(Action<Type, TContext> handler);

        /// <summary>
        /// Returns <c>true</c> when <paramref name="type"/> passes all registered filters; <c>false</c> otherwise.
        /// When no filter is defined, all implementations should return <c>true</c>.
        /// </summary>
        /// <param name="type">Type to test.</param>
        /// <param name="context">Processing context.</param>
        /// <returns>Returns <c>true</c> when <paramref name="type"/> passes all registered filters; <c>false</c> otherwise.</returns>
        bool IsMatched(Type type, TContext context);

        /// <summary>
        /// If <paramref name="type"/> passes all registered filters, then all registered handlers are executed.
        /// </summary>
        /// <param name="type">Type to process.</param>
        /// <param name="context">Processing context.</param>
        void Handle(Type type, TContext context);
    }

    public interface ITypeEnumerator
    {
        /// <summary>
        /// Adds filters. All filters and conjucted.
        /// </summary>
        /// <param name="filter">Delegate, that returns <c>true</c> to pass type handler and <c>false</c> to filter out passed type.</param>
        /// <returns>Self (for fluency).</returns>
        ITypeEnumerator AddFilter(Func<Type, bool> filter);

        /// <summary>
        /// Adds handler, that will be execute for every type, that matches all filters.
        /// </summary>
        /// <param name="handler">Delegate to be executed for every type, that matches all filters.</param>
        /// <returns>Self (for fluency).</returns>
        ITypeEnumerator AddHandler(Action<Type> handler);

        /// <summary>
        /// Returns <c>true</c> when <paramref name="type"/> passes all registered filters; <c>false</c> otherwise.
        /// When no filter is defined, all implementations should return <c>true</c>.
        /// </summary>
        /// <param name="type">Type to test.</param>
        /// <returns>Returns <c>true</c> when <paramref name="type"/> passes all registered filters; <c>false</c> otherwise.</returns>
        bool IsMatched(Type type);

        /// <summary>
        /// If <paramref name="type"/> passes all registered filters, then all registered handlers are executed.
        /// </summary>
        /// <param name="type">Type to process.</param>
        void Handle(Type type);
    }
}
