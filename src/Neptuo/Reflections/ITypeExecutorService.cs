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
    /// Service for registering <see cref="ITypeExecutor"/>.
    /// On dispose, all registered executors will be run on all loaded types.
    /// </summary>
    public interface ITypeExecutorService : IDisposable
    {
        /// <summary>
        /// Adds type executor, that will be executed on dispose
        /// and if <paramref name="isExecutedForLatelyLoadedAssemblies" /> is <c>true</c> then also for all lately loaded assemblies.
        /// </summary>
        /// <param name="executor">Type enumerator.</param>
        /// <param name="isExecutedForLatelyLoadedAssemblies">Whether to execute <paramref name="executor"/> also for lately loaded assemblies.</param>
        ITypeExecutorService AddTypeExecutor(ITypeExecutor executor, bool isExecutedForLatelyLoadedAssemblies);
    }
}
