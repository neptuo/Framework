using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Reflection
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
    }
}
