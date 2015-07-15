using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Reflections.Enumerators
{
    /// <summary>
    /// Common extensions for <see cref="ITypeEnumerator"/> and <see cref="ITypeEnumerator{TContext}"/>.
    /// </summary>
    public static class _TypeEnumeratorExtensions
    {
        #region AddFilterNotInterface

        /// <summary>
        /// Adds filter on <paramref name="enumerator"/> to filter out interfaces.
        /// </summary>
        /// <param name="enumerator">Type enumerator.</param>
        /// <returns>Result from calling <see cref="ITypeEnumerator.AddFilter"/>.</returns>
        public static ITypeEnumerator AddFilterNotInterface(this ITypeEnumerator enumerator)
        {
            Ensure.NotNull(enumerator, "enumerator");
            return enumerator.AddFilter(t => !t.IsInterface);
        }

        /// <summary>
        /// Adds filter on <paramref name="enumerator"/> to filter out interfaces.
        /// </summary>
        /// <param name="enumerator">Type enumerator.</param>
        /// <returns>Result from calling <see cref="ITypeEnumerator.AddFilter"/>.</returns>
        public static ITypeEnumerator<TContext> AddFilterNotInterface<TContext>(this ITypeEnumerator<TContext> enumerator)
        {
            Ensure.NotNull(enumerator, "enumerator");
            return enumerator.AddFilter((t, context) => !t.IsInterface);
        }

        #endregion

        #region AddTypeFilterNotAbstract

        /// <summary>
        /// Adds filter on <paramref name="enumerator"/> to filter out abstract classes.
        /// </summary>
        /// <param name="enumerator">Type enumerator.</param>
        /// <returns>Result from calling <see cref="ITypeEnumerator.AddFilter"/>.</returns>
        public static ITypeEnumerator AddTypeFilterNotAbstract(this ITypeEnumerator enumerator)
        {
            Ensure.NotNull(enumerator, "enumerator");
            return enumerator.AddFilter(t => !t.IsInterface);
        }

        /// <summary>
        /// Adds filter on <paramref name="enumerator"/> to filter out abstract classes.
        /// </summary>
        /// <param name="enumerator">Type enumerator.</param>
        /// <returns>Result from calling <see cref="ITypeEnumerator.AddFilter"/>.</returns>
        public static ITypeEnumerator<TContext> AddTypeFilterNotAbstract<TContext>(this ITypeEnumerator<TContext> enumerator)
        {
            Ensure.NotNull(enumerator, "enumerator");
            return enumerator.AddFilter((t, context) => !t.IsAbstract);
        }

        #endregion
    }
}
