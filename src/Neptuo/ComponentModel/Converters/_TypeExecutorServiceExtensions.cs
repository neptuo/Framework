using Neptuo.ComponentModel.Converters;
using Neptuo.Reflections;
using Neptuo.Reflections.Enumerators;
using Neptuo.Reflections.Enumerators.Executors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.ComponentModel.Converters
{
    /// <summary>
    /// Auto-wiring converter extensions for <see cref="ITypeExecutorService"/>.
    /// </summary>
    public static class _TypeExecutorServiceExtensions
    {
        /// <summary>
        /// Adds all types found <paramref name="service"/> and marked with <see cref="ConverterAttribute"/> to the <see cref="Converts.Repository"/>.
        /// </summary>
        /// <param name="service">Type enumerating service.</param>
        /// <param name="repository">Repository to add converters to.</param>
        /// <param name="isExecutedForLatelyLoadedAssemblies">Whether to add converters from lately loaded assemblies.</param>
        /// <returns><paramref name="service"/>.</returns>
        public static ITypeExecutorService AddConverters(this ITypeExecutorService service, bool isExecutedForLatelyLoadedAssemblies = true)
        {
            return AddConverters(service, Converts.Repository, isExecutedForLatelyLoadedAssemblies);
        }

        /// <summary>
        /// Adds all types found <paramref name="service"/> and marked with <see cref="ConverterAttribute"/> to the <paramref name="repository"/>.
        /// </summary>
        /// <param name="service">Type enumerating service.</param>
        /// <param name="repository">Repository to add converters to.</param>
        /// <param name="isExecutedForLatelyLoadedAssemblies">Whether to add converters from lately loaded assemblies.</param>
        /// <returns><paramref name="service"/>.</returns>
        public static ITypeExecutorService AddConverters(this ITypeExecutorService service, IConverterRepository repository, bool isExecutedForLatelyLoadedAssemblies = true)
        {
            Ensure.NotNull(service, "service");
            Ensure.NotNull(repository, "repository");
            service
                .AddFiltered(isExecutedForLatelyLoadedAssemblies)
                .AddFilterNotInterface()
                .AddFilterNotAbstract()
                .AddFilterHasAttribute<ConverterAttribute>()
                .AddHandler(t => AddConverter(repository, t));

            return service;
        }

        private static void AddConverter(IConverterRepository repository, Type converterType)
        {
            IEnumerable<object> attributes = converterType.GetCustomAttributes(typeof(ConverterAttribute), true);
            IConverter converter = (IConverter)Activator.CreateInstance(converterType);
            foreach (ConverterAttribute attribute in attributes)
            {
                if (attribute.HasTypesDefined)
                {
                    Type sourceType = attribute.SourceType;
                    Type targetType = attribute.TargetType;
                    repository.Add(sourceType, targetType, converter);
                }
                else
                {
                    IEnumerable<Type> interfaceTypes = converterType.GetInterfaces();
                    foreach (Type interfaceType in interfaceTypes)
                    {
                        if (interfaceType.IsGenericType && typeof(IConverter<,>) == interfaceType.GetGenericTypeDefinition())
                        {
                            Type[] parameters = interfaceType.GetGenericArguments();
                            Type sourceType = parameters[0];
                            Type targetType = parameters[1];
                            repository.Add(sourceType, targetType, converter);
                        }
                    }
                }
            }
        }
    }
}
