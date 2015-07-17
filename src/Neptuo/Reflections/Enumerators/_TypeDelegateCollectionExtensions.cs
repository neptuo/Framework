using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Reflections.Enumerators
{
    /// <summary>
    /// Common extensions for <see cref="ITypeDelegateCollection"/> and <see cref="ITypeDelegateCollection{TContext}"/>.
    /// </summary>
    public static class _TypeDelegateCollectionExtensions
    {
        #region AddFilterNotInterface

        /// <summary>
        /// Adds filter on <paramref name="enumerator"/> to filter out interfaces.
        /// </summary>
        /// <param name="enumerator">Type enumerator.</param>
        /// <returns>Result from calling <see cref="ITypeDelegateCollection.AddFilter"/>.</returns>
        public static ITypeDelegateCollection AddFilterNotInterface(this ITypeDelegateCollection enumerator)
        {
            Ensure.NotNull(enumerator, "enumerator");
            return enumerator.AddFilter(t => !t.IsInterface);
        }

        /// <summary>
        /// Adds filter on <paramref name="enumerator"/> to filter out interfaces.
        /// </summary>
        /// <param name="enumerator">Type enumerator.</param>
        /// <returns>Result from calling <see cref="ITypeDelegateCollection.AddFilter"/>.</returns>
        public static ITypeDelegateCollection<TContext> AddFilterNotInterface<TContext>(this ITypeDelegateCollection<TContext> enumerator)
        {
            Ensure.NotNull(enumerator, "enumerator");
            return enumerator.AddFilter((t, context) => !t.IsInterface);
        }

        #endregion

        #region AddFilterNotAbstract

        /// <summary>
        /// Adds filter on <paramref name="enumerator"/> to filter out abstract classes.
        /// </summary>
        /// <param name="enumerator">Type enumerator.</param>
        /// <returns>Result from calling <see cref="ITypeDelegateCollection.AddFilter"/>.</returns>
        public static ITypeDelegateCollection AddFilterNotAbstract(this ITypeDelegateCollection enumerator)
        {
            Ensure.NotNull(enumerator, "enumerator");
            return enumerator.AddFilter(t => !t.IsInterface);
        }

        /// <summary>
        /// Adds filter on <paramref name="enumerator"/> to filter out abstract classes.
        /// </summary>
        /// <param name="enumerator">Type enumerator.</param>
        /// <returns>Result from calling <see cref="ITypeDelegateCollection.AddFilter"/>.</returns>
        public static ITypeDelegateCollection<TContext> AddFilterNotAbstract<TContext>(this ITypeDelegateCollection<TContext> enumerator)
        {
            Ensure.NotNull(enumerator, "enumerator");
            return enumerator.AddFilter((t, context) => !t.IsAbstract);
        }

        #endregion
    }
}
