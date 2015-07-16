using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Reflections.Enumerators
{
    /// <summary>
    /// Contract for processing types.
    /// </summary>
    /// <typeparam name="TContext">Type of context information.</typeparam>
    public interface ITypeExecutor<TContext>
    {
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

    /// <summary>
    /// Contract for processing types.
    /// </summary>
    public interface ITypeExecutor
    {
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
