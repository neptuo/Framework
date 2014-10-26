using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Compilers
{
    /// <summary>
    /// Some utilities for compiling and loading code on the fly.
    /// </summary>
    public class CompilerService
    {
        private readonly AppDomain appDomain;

        /// <summary>
        /// Current compiler factory.
        /// </summary>
        public CompilerFactory Factory { get; private set; }

        /// <summary>
        /// Creates instance for <paramref name="appDomain"/>.
        /// </summary>
        /// <param name="appDomain">Application domain for loading assemblies into.</param>
        private CompilerService(AppDomain appDomain)
        {
            Guard.NotNull(appDomain, "appDomain");
            this.appDomain = appDomain;
            Factory = new CompilerFactory();
        }

        /// <summary>
        /// Loads assembly to current application domain.
        /// </summary>
        /// <param name="assemblyFile"></param>
        /// <returns></returns>
        public Assembly LoadAssembly(string assemblyFile)
        {
            Guard.NotNullOrEmpty(assemblyFile, "assemblyFile");
            if (!File.Exists(assemblyFile))
                throw Guard.Exception.ArgumentFileNotExist("assemblyFile", assemblyFile);

            AssemblyName assemblyName = AssemblyName.GetAssemblyName(assemblyFile);
            Assembly assembly = appDomain.Load(File.ReadAllBytes(assemblyFile));

            return assembly;
        }

        /// <summary>
        /// Creates compiler service for <paramref name="appDomain"/>.
        /// </summary>
        /// <param name="appDomain">Application domain for loading assemblies into.</param>
        /// <returns>Created compiler service.</returns>
        public static CompilerService FromAppDomain(AppDomain appDomain)
        {
            return new CompilerService(appDomain);
        }

        /// <summary>
        /// Creates compiler service for current app domain.
        /// </summary>
        /// <returns>Created compiler service.</returns>
        public static CompilerService FromCurrentAppDomain()
        {
            return FromAppDomain(AppDomain.CurrentDomain);
        }
    }
}
