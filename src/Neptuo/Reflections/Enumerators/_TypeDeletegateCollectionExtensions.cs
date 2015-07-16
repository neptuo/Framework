using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Reflections.Enumerators
{
    /// <summary>
    /// Common extensions for <see cref="ITypeDeletegateCollection"/> and <see cref="ITypeDeletegateCollection{TContext}"/>.
    /// </summary>
    public static class _TypeDeletegateCollectionExtensions
    {
        #region AddFilterNotInterface

        /// <summary>
        /// Adds filter on <paramref name="enumerator"/> to filter out interfaces.
        /// </summary>
        /// <param name="enumerator">Type enumerator.</param>
        /// <returns>Result from calling <see cref="ITypeDeletegateCollection.AddFilter"/>.</returns>
        public static ITypeDeletegateCollection AddFilterNotInterface(this ITypeDeletegateCollection enumerator)
        {
            Ensure.NotNull(enumerator, "enumerator");
            return enumerator.AddFilter(t => !t.IsInterface);
        }

        /// <summary>
        /// Adds filter on <paramref name="enumerator"/> to filter out interfaces.
        /// </summary>
        /// <param name="enumerator">Type enumerator.</param>
        /// <returns>Result from calling <see cref="ITypeDeletegateCollection.AddFilter"/>.</returns>
        public static ITypeDeletegateCollection<TContext> AddFilterNotInterface<TContext>(this ITypeDeletegateCollection<TContext> enumerator)
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
        /// <returns>Result from calling <see cref="ITypeDeletegateCollection.AddFilter"/>.</returns>
        public static ITypeDeletegateCollection AddTypeFilterNotAbstract(this ITypeDeletegateCollection enumerator)
        {
            Ensure.NotNull(enumerator, "enumerator");
            return enumerator.AddFilter(t => !t.IsInterface);
        }

        /// <summary>
        /// Adds filter on <paramref name="enumerator"/> to filter out abstract classes.
        /// </summary>
        /// <param name="enumerator">Type enumerator.</param>
        /// <returns>Result from calling <see cref="ITypeDeletegateCollection.AddFilter"/>.</returns>
        public static ITypeDeletegateCollection<TContext> AddTypeFilterNotAbstract<TContext>(this ITypeDeletegateCollection<TContext> enumerator)
        {
            Ensure.NotNull(enumerator, "enumerator");
            return enumerator.AddFilter((t, context) => !t.IsAbstract);
        }

        #endregion
    }
}
