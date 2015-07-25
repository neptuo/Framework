using Neptuo.Activators;
using Neptuo.Reflections;
using Neptuo.Reflections.Enumerators;
using Neptuo.Reflections.Enumerators.Executors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Services.Queries.Handlers.AutoExports
{
    /// <summary>
    /// Auto-wiring query handlers extensions for <see cref="ITypeExecutorService"/>.
    /// </summary>
    public static class _TypeExecutorServiceExtensions
    {
        /// <summary>
        /// Adds all types found <paramref name="service"/> and marked with <see cref="ConverterAttribute"/> to the <paramref name="repository"/>.
        /// </summary>
        /// <param name="service">Type enumerating service.</param>
        /// <param name="dependencyContainer">Container to register handlers to.</param>
        /// <param name="isExecutedForLatelyLoadedAssemblies">Whether to add query handlers from lately loaded assemblies.</param>
        /// <returns><paramref name="service"/>.</returns>
        public static ITypeExecutorService AddQueryHandlers(this ITypeExecutorService service, IDependencyContainer dependencyContainer, bool isExecutedForLatelyLoadedAssemblies = true)
        {
            Ensure.NotNull(service, "service");
            Ensure.NotNull(dependencyContainer, "dependencyContainer");
            service
                .AddFiltered(isExecutedForLatelyLoadedAssemblies)
                .AddFilterNotInterface()
                .AddFilterNotAbstract()
                .AddFilterHasAttribute<QueryHandlerAttribute>()
                .AddHandler(t => AddQueryHandler(dependencyContainer, t));

            return service;
        }

        private static void AddQueryHandler(IDependencyContainer dependencyContainer, Type queryHandlerType)
        {
            IEnumerable<object> attributes = queryHandlerType.GetCustomAttributes(typeof(QueryHandlerAttribute), true);
            foreach (QueryHandlerAttribute attribute in attributes)
            {
                if (attribute.HasTypeDefined)
                {
                    Type queryType = attribute.QueryType;
                    IEnumerable<Type> queryInterfaceTypes = queryType.GetInterfaces();
                    foreach (Type queryInterfaceType in queryInterfaceTypes)
                    {
                        if (queryInterfaceType.IsGenericType)
                        {
                            Type[] parameters = queryInterfaceType.GetGenericArguments();
                            if (parameters.Length == 1)
                            {
                                Type queryResultType = parameters[0];
                                Type queryHandlerInterfaceType = typeof(IQueryHandler<,>).MakeGenericType(queryType, queryResultType);
                                dependencyContainer.Definitions.Add(queryHandlerInterfaceType, DependencyLifetime.Transient, queryHandlerType);
                            }
                        }
                    }

                    //TODO: Implement else branches...
                }
                else
                {
                    IEnumerable<Type> interfaceTypes = queryHandlerType.GetInterfaces();
                    foreach (Type interfaceType in interfaceTypes)
                    {
                        if (interfaceType.IsGenericType && typeof(IQueryHandler<,>) == interfaceType.GetGenericTypeDefinition())
                        {
                            Type[] parameters = interfaceType.GetGenericArguments();
                            Type queryHandlerInterfaceType = typeof(IQueryHandler<,>).MakeGenericType(parameters[0], parameters[1]);
                            dependencyContainer.Definitions.Add(queryHandlerInterfaceType, DependencyLifetime.Transient, queryHandlerType);
                        }
                    }
                }
            }
        }
    }
}
