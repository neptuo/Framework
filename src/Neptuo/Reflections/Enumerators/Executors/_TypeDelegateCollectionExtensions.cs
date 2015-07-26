using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Reflections.Enumerators.Executors
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
        /// <typeparam name="TContext">Type of context.</typeparam>
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
            return enumerator.AddFilter(t => !t.IsAbstract);
        }

        /// <summary>
        /// Adds filter on <paramref name="enumerator"/> to filter out abstract classes.
        /// </summary>
        /// <typeparam name="TContext">Type of context.</typeparam>
        /// <param name="enumerator">Type enumerator.</param>
        /// <returns>Result from calling <see cref="ITypeDelegateCollection.AddFilter"/>.</returns>
        public static ITypeDelegateCollection<TContext> AddFilterNotAbstract<TContext>(this ITypeDelegateCollection<TContext> enumerator)
        {
            Ensure.NotNull(enumerator, "enumerator");
            return enumerator.AddFilter((t, context) => !t.IsAbstract);
        }

        #endregion

        #region AddFilterHasAttribute

        /// <summary>
        /// Adds filter on <paramref name="enumerator"/> to filter out type that doesn't have <paramref name="attributeType" />.
        /// </summary>
        /// <param name="enumerator">Type enumerator.</param>
        /// <param name="attributeType">Required attribute type.</param>
        /// <returns>Result from calling <see cref="ITypeDelegateCollection.AddFilter"/>.</returns>
        public static ITypeDelegateCollection AddFilterHasAttribute(this ITypeDelegateCollection enumerator, Type attributeType)
        {
            Ensure.NotNull(enumerator, "enumerator");
            return enumerator.AddFilter(t => t.GetCustomAttributes(attributeType, true).Length > 0);
        }

        /// <summary>
        /// Adds filter on <paramref name="enumerator"/> to filter out type that doesn't have <typeparamref name="TAttribute"/>.
        /// </summary>
        /// <typeparam name="TAttribute">Required attribute type.</typeparam>
        /// <param name="enumerator">Type enumerator.</param>
        /// <returns>Result from calling <see cref="ITypeDelegateCollection.AddFilter"/>.</returns>
        public static ITypeDelegateCollection AddFilterHasAttribute<TAttribute>(this ITypeDelegateCollection enumerator)
        {
            return AddFilterHasAttribute(enumerator, typeof(TAttribute));
        }

        /// <summary>
        /// Adds filter on <paramref name="enumerator"/> to filter out type that doesn't have <paramref name="attributeType" />.
        /// </summary>
        /// <typeparam name="TContext">Type of context.</typeparam>
        /// <param name="enumerator">Type enumerator.</param>
        /// <param name="attributeType">Required attribute type.</param>
        /// <returns>Result from calling <see cref="ITypeDelegateCollection.AddFilter"/>.</returns>
        public static ITypeDelegateCollection<TContext> AddFilterHasAttribute<TContext>(this ITypeDelegateCollection<TContext> enumerator, Type attributeType)
        {
            Ensure.NotNull(enumerator, "enumerator");
            return enumerator.AddFilter((t, context) => t.GetCustomAttributes(attributeType, true).Length > 0);
        }

        /// <summary>
        /// Adds filter on <paramref name="enumerator"/> to filter out type that doesn't have <typeparamref name="TAttribute"/>.
        /// </summary>
        /// <typeparam name="TContext">Type of context.</typeparam>
        /// <typeparam name="TAttribute">Required attribute type.</typeparam>
        /// <param name="enumerator">Type enumerator.</param>
        /// <returns>Result from calling <see cref="ITypeDelegateCollection.AddFilter"/>.</returns>
        public static ITypeDelegateCollection<TContext> AddFilterHasAttribute<TContext, TAttribute>(this ITypeDelegateCollection<TContext> enumerator)
        {
            return AddFilterHasAttribute(enumerator, typeof(TAttribute));
        }

        #endregion

        #region AddFilterHasConstructor

        /// <summary>
        /// Adds filter on <paramref name="enumerator"/> to filter out classes without constructor with <paramref name="parameterTypes" />.
        /// </summary>
        /// <param name="enumerator">Type enumerator.</param>
        /// <param name="parameterTypes">Ordered array of required parameters in constructor.</param>
        /// <returns>Result from calling <see cref="ITypeDelegateCollection.AddFilter"/>.</returns>
        public static ITypeDelegateCollection AddFilterHasConstructor(this ITypeDelegateCollection enumerator, params Type[] parameterTypes)
        {
            Ensure.NotNull(enumerator, "enumerator");
            return enumerator.AddFilter(t => t.GetConstructor(parameterTypes) != null);
        }

        /// <summary>
        /// Adds filter on <paramref name="enumerator"/> to filter out classes without constructor with <paramref name="parameterTypes" />.
        /// </summary>
        /// <typeparam name="TContext">Type of context.</typeparam>
        /// <param name="enumerator">Type enumerator.</param>
        /// <param name="parameterTypes">Ordered array of required parameters in constructor.</param>
        /// <returns>Result from calling <see cref="ITypeDelegateCollection.AddFilter"/>.</returns>
        public static ITypeDelegateCollection<TContext> AddFilterHasConstructor<TContext>(this ITypeDelegateCollection<TContext> enumerator, params Type[] parameterTypes)
        {
            Ensure.NotNull(enumerator, "enumerator");
            return enumerator.AddFilter((t, context) => t.GetConstructor(parameterTypes) != null);
        }

        #endregion

        #region AddFilterHasDefaultConstructor

        /// <summary>
        /// Adds filter on <paramref name="enumerator"/> to filter out classes without default (parameter-less) constructor.
        /// </summary>
        /// <param name="enumerator">Type enumerator.</param>
        /// <returns>Result from calling <see cref="ITypeDelegateCollection.AddFilter"/>.</returns>
        public static ITypeDelegateCollection AddFilterHasDefaultConstructor(this ITypeDelegateCollection enumerator)
        {
            Ensure.NotNull(enumerator, "enumerator");
            return enumerator.AddFilter(t => t.HasDefaultConstructor());
        }

        /// <summary>
        /// Adds filter on <paramref name="enumerator"/> to filter out classes without default (parameter-less) constructor.
        /// </summary>
        /// <typeparam name="TContext">Type of context.</typeparam>
        /// <param name="enumerator">Type enumerator.</param>
        /// <returns>Result from calling <see cref="ITypeDelegateCollection.AddFilter"/>.</returns>
        public static ITypeDelegateCollection<TContext> AddFilterHasDefaultConstructor<TContext>(this ITypeDelegateCollection<TContext> enumerator)
        {
            Ensure.NotNull(enumerator, "enumerator");
            return enumerator.AddFilter((t, context) => t.HasDefaultConstructor());
        }

        #endregion
    }
}
