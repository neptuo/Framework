using Neptuo.Reflections.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Reflections
{
    /// <summary>
    /// Some usefull shortcuts in reflection.
    /// </summary>
    public interface IReflectionService
    {
        /// <summary>
        /// Application domain on which this service operates.
        /// </summary>
        AppDomain AppDomain { get; }

        /// <summary>
        /// Enumerates assemblies in this application domain.
        /// </summary>
        /// <returns>Enumeration of assemblies in this application domain.</returns>
        IEnumerable<Assembly> EnumerateAssemblies();

        /// <summary>
        /// Loads assembly to this application domain.
        /// </summary>
        /// <param name="assemblyFile"></param>
        /// <returns></returns>
        Assembly LoadAssembly(string assemblyFile);

        /// <summary>
        /// Loads type described by assembly qualified name in <paramref name="typeAssemblyName"/>.
        /// </summary>
        /// <param name="typeAssemblyName">Assembly qualified name of the type to load.</param>
        /// <returns>Type described by assembly qualified name in <paramref name="typeAssemblyName"/></returns>
        Type LoadType(string typeAssemblyName);

        /// <summary>
        /// Adds type executor, that will be executed immediately 
        /// and if <paramref name="isExecutedForLatelyLoadedAssemblies" /> is <c>true</c> then also for all lately loaded assemblies.
        /// </summary>
        /// <param name="executor">Type enumerator.</param>
        /// <param name="isExecutedForLatelyLoadedAssemblies">Whether to execute <paramref name="executor"/> also for lately loaded assemblies.</param>
        void AddTypeExecutor(ITypeExecutor executor, bool isExecutedForLatelyLoadedAssemblies);
    }
}
