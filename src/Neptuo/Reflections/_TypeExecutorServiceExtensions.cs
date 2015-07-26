using Neptuo.Reflections.Enumerators;
using Neptuo.Reflections.Enumerators.Executors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Reflections
{
    /// <summary>
    /// Common extensions for <see cref="ITypeExecutorService"/>.
    /// </summary>
    public static class _TypeExecutorServiceExtensions
    {
        /// <summary>
        /// Adds <see cref="FilterTypeExecutor"/> to the <paramref name="executorService"/>.
        /// </summary>
        /// <param name="executorService">Target service.</param>
        /// <param name="isExecutedForLatelyLoadedAssemblies">Whether to execute <paramref name="executor"/> also for lately loaded assemblies.</param>
        /// <returns>Newly created instance of <see cref="FilterTypeExecutor"/>.</returns>
        public static FilterTypeExecutor AddFiltered(this ITypeExecutorService executorService, bool isExecutedForLatelyLoadedAssemblies)
        {
            Ensure.NotNull(executorService, "executorService");
            FilterTypeExecutor executor = new FilterTypeExecutor();
            executorService.AddTypeExecutor(executor, isExecutedForLatelyLoadedAssemblies);
            return executor;
        }
    }
}
