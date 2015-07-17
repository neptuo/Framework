using Neptuo.Reflections.Enumerators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Reflections
{
    /// <summary>
    /// Default implementation of <see cref="IReflectionService"/>.
    /// </summary>
    internal class DefaultReflectionService : IReflectionService
    {
        private readonly List<ITypeExecutor> typeExecutors = new List<ITypeExecutor>();
        private bool isAssemblyLoadedAttached;

        public AppDomain AppDomain { get; private set; }

        /// <summary>
        /// Creates instance for <paramref name="appDomain"/>.
        /// </summary>
        /// <param name="appDomain">Application domain for loading assemblies into.</param>
        internal DefaultReflectionService(AppDomain appDomain)
        {
            Ensure.NotNull(appDomain, "appDomain");
            AppDomain = appDomain;
        }

        public IEnumerable<Assembly> EnumerateAssemblies()
        {
            return AppDomain.GetAssemblies().Where(a => !a.IsDynamic);
        }

        public Assembly LoadAssembly(string assemblyFile)
        {
            Ensure.NotNullOrEmpty(assemblyFile, "assemblyFile");
            if (!File.Exists(assemblyFile))
                throw Ensure.Exception.ArgumentFileNotExist(assemblyFile, "assemblyFile");

            Assembly assembly = AppDomain.Load(File.ReadAllBytes(assemblyFile));
            return assembly;
        }

        public Type LoadType(string typeAssemblyName)
        {
            Ensure.NotNullOrEmpty(typeAssemblyName, "typeAssemblyName");
            string[] parts = typeAssemblyName.Split(',');
            string typeName = parts[0].Trim();
            string assemblyName = null;
            if (parts.Length == 2)
                assemblyName = parts[1].Trim();

            IEnumerable<Assembly> searchIn = EnumerateAssemblies().Where(a => a.GetName().Name == assemblyName);
            if (!searchIn.Any())
            {
                if (assemblyName != null)
                {
                    Assembly assembly = AppDomain.Load(assemblyName);
                    searchIn = new List<Assembly>() { assembly };
                }
                else
                {
                    searchIn = EnumerateAssemblies();
                }
            }

            foreach (Assembly assembly in searchIn)
            {
                Type type = assembly.GetType(typeName);
                if (type != null)
                    return type;
            }

            return null;
        }


        public void AddTypeExecutor(ITypeExecutor executor, bool isExecutedForLatelyLoadedAssemblies)
        {
            Ensure.NotNull(executor, "executor");

            foreach (Assembly assembly in EnumerateAssemblies())
            {
                AssemblyTypeEnumerator enumerator = new AssemblyTypeEnumerator(assembly);
                enumerator.HandleExecutor(executor);
            }

            if(isExecutedForLatelyLoadedAssemblies)
            {
                typeExecutors.Add(executor);
                EnsureAssemblyLoadDelegate();
            }
        }

        private void EnsureAssemblyLoadDelegate()
        {
            if (!isAssemblyLoadedAttached)
            {
                AppDomain.AssemblyLoad += OnAssemblyLoaded;
                isAssemblyLoadedAttached = true;
            }
        }

        private void OnAssemblyLoaded(object sender, AssemblyLoadEventArgs args)
        {
            foreach (ITypeExecutor executor in typeExecutors)
            {
                AssemblyTypeEnumerator enumerator = new AssemblyTypeEnumerator(args.LoadedAssembly);
                enumerator.HandleExecutor(executor);
            }
        }
    }
}
