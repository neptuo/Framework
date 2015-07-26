using Neptuo.Activators;
using Neptuo.Activators.AutoExports;
using Neptuo.Reflections;
using Neptuo.Reflections.Enumerators;
using Neptuo.Reflections.Enumerators.Executors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        /// Adds all types found <paramref name="service"/> and marked with <see cref="QueryHandlerAttribute"/> to the <paramref name="dependencyContainer"/>.
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
            IEnumerable<object> allAttributes = queryHandlerType.GetCustomAttributes(true);
            ExportLifetimeAttribute lifetimeAttribute = allAttributes.OfType<ExportLifetimeAttribute>().FirstOrDefault();
            DependencyLifetime lifetime = lifetimeAttribute != null ? lifetimeAttribute.GetLifetime() : DependencyLifetime.Transient;

            IEnumerable<object> attributes = allAttributes.OfType<QueryHandlerAttribute>();
            foreach (Type queryHandlerInterfaceType in GetHandlerInterfaces(queryHandlerType, attributes))
                dependencyContainer.Definitions.Add(queryHandlerInterfaceType, lifetime, queryHandlerType);
        }

        /// <summary>
        /// Adds all types found <paramref name="service"/> and marked with <see cref="QueryHandlerAttribute"/> to the <paramref name="collection"/>.
        /// </summary>
        /// <param name="service">Type enumerating service.</param>
        /// <param name="collection">Query handler collection to add handlers to..</param>
        /// <param name="isExecutedForLatelyLoadedAssemblies">Whether to add query handlers from lately loaded assemblies.</param>
        /// <returns><paramref name="service"/>.</returns>
        public static ITypeExecutorService AddQueryHandlers(this ITypeExecutorService service, IQueryHandlerCollection collection, bool isExecutedForLatelyLoadedAssemblies = true)
        {
            Ensure.NotNull(service, "service");
            Ensure.NotNull(collection, "collection");
            service
                .AddFiltered(isExecutedForLatelyLoadedAssemblies)
                .AddFilterNotInterface()
                .AddFilterNotAbstract()
                .AddFilterHasDefaultConstructor()
                .AddFilterHasAttribute<QueryHandlerAttribute>()
                .AddHandler(t => AddQueryHandler(collection, t));

            return service;
        }

        private static string addMethodName = "Add";

        private static void AddQueryHandler(IQueryHandlerCollection collection, Type queryHandlerType)
        {
            IEnumerable<object> attributes = queryHandlerType.GetCustomAttributes(typeof(QueryHandlerAttribute), true);
            object handler = Activator.CreateInstance(queryHandlerType);
            foreach (Type queryHandlerInterfaceType in GetHandlerInterfaces(queryHandlerType, attributes))
            {
                MethodInfo addMethod = collection.GetType().GetMethod(addMethodName).MakeGenericMethod(queryHandlerInterfaceType.GetGenericArguments());
                addMethod.Invoke(collection, new object[] { handler });
            }
        }

        #region Attribute parsing extensions

        public static IEnumerable<Type> GetHandlerInterfaces(Type queryHandlerType, IEnumerable<object> queryHandlerAttributes)
        {
            List<Type> result = new List<Type>();
            foreach (QueryHandlerAttribute attribute in queryHandlerAttributes)
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
                                result.Add(queryHandlerInterfaceType);
                            }
                        }
                    }
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
                            result.Add(queryHandlerInterfaceType);
                        }
                    }
                }
            }

            return result;
        }

        #endregion
    }
}
