using Neptuo.Reflections.Enumerators;
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
        public static FilterTypeExecutor AddFiltered(this ITypeExecutorService executorService, bool isExecutedForLatelyLoadedAssemblies)
        {
            Ensure.NotNull(executorService, "executorService");
            FilterTypeExecutor executor = new FilterTypeExecutor();
            executorService.AddTypeExecutor(executor, isExecutedForLatelyLoadedAssemblies);
            return executor;
        }
    }
}
