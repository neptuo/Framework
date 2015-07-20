using Neptuo.Reflections.Enumerators.Executors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Reflections.Enumerators
{
    /// <summary>
    /// Common extesnions for <see cref="ITypeEnumerator"/>.
    /// </summary>
    public static class _TypeEnumeratorExtensions
    {
        /// <summary>
        /// Executes <paramref name="executor"/> on all types from <paramref name="enumerator"/>.
        /// </summary>
        /// <param name="enumerator">Type enumerator.</param>
        /// <param name="executor">Type executor.</param>
        public static void HandleExecutor(this ITypeEnumerator enumerator, ITypeExecutor executor)
        {
            Ensure.NotNull(enumerator, "enumerator");
            Ensure.NotNull(executor, "executor");

            while (enumerator.Next())
                executor.Handle(enumerator.Current);
        }
    }
}
