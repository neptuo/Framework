using Neptuo.Reflections;
using Neptuo.Reflections.Enumerators.Executors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators.AutoExports
{
    /// <summary>
    /// Dependency container extensions for <see cref="ITypeExecutorService"/>.
    /// </summary>
    public static class _TypeExecutorServiceExtensions
    {
        /// <summary>
        /// Adds all types found <paramref name="service"/> and marked with <see cref="ConverterAttribute"/> to the <paramref name="repository"/>.
        /// </summary>
        /// <param name="service">Type enumerating service.</param>
        /// <param name="repository">Repository to add converters to.</param>
        /// <param name="isExecutedForLatelyLoadedAssemblies">Whether to add converters from lately loaded assemblies.</param>
        /// <returns><paramref name="service"/>.</returns>
        public static ITypeExecutorService AddDependencies(this ITypeExecutorService service, IDependencyContainer dependencyContainer, bool isExecutedForLatelyLoadedAssemblies = true)
        {
            Ensure.NotNull(service, "service");
            Ensure.NotNull(dependencyContainer, "dependencyContainer");
            service
                .AddFiltered(isExecutedForLatelyLoadedAssemblies)
                .AddFilterNotInterface()
                .AddFilterNotAbstract()
                .AddFilterHasAttribute<ExportAttribute>()
                .AddHandler(t => AddExport(dependencyContainer, t));

            return service;
        }

        private static void AddExport(IDependencyContainer dependencyContainer, Type targetType)
        {
            IEnumerable<object> allAttributes = targetType.GetCustomAttributes(true);
            ExportLifetimeAttribute lifetimeAttribute = allAttributes.OfType<ExportLifetimeAttribute>().FirstOrDefault();
            DependencyLifetime lifetime = lifetimeAttribute != null ? lifetimeAttribute.GetLifetime() : DependencyLifetime.Transient;

            IEnumerable<object> exportAttributes = allAttributes.OfType<ExportAttribute>();
            foreach (ExportAttribute attribute in exportAttributes)
            {
                dependencyContainer.Definitions
                    .Add(attribute.RequiredType, lifetime, targetType);
            }
        }
    }
}
