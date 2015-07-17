using Neptuo.Reflections.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Reflections
{
    /// <summary>
    /// Common extensions for <see cref="IReflectionService"/>.
    /// </summary>
    public static class _ReflectionServiceExtensions
    {
        public static FilterTypeExecutor AddFilterTypeExecutor(this IReflectionService reflectionService, bool isExecutedForLatelyLoadedAssemblies)
        {
            Ensure.NotNull(reflectionService, "reflectionService");
            FilterTypeExecutor executor = new FilterTypeExecutor();
            reflectionService.AddTypeExecutor(executor, isExecutedForLatelyLoadedAssemblies);
            return executor;
        }
    }
}
