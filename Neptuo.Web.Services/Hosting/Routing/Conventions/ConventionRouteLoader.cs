using Neptuo.Web.Services.Hosting.Http;
using Neptuo.Web.Services.Hosting.Pipelines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Services.Hosting.Routing.Conventions
{
    /// <summary>
    /// Loades types according to specified convensions, <see cref="IConvention"/>.
    /// </summary>
    public class ConventionRouteLoader
    {
        /// <summary>
        /// Route table to register routes to.
        /// </summary>
        protected IRouteTable RouteTable { get; private set; }

        /// <summary>
        /// Mapper between handler type and its pipeline factory.
        /// </summary>
        protected Func<Type, IPipelineFactory> PipelineMapper { get; private set; }

        /// <summary>
        /// Collection of registered convensions.
        /// </summary>
        public ICollection<IConvention> Convensions { get; private set; }

        /// <summary>
        /// Creates new instance that registers routes to <paramref name="routeTable"/> 
        /// and uses <paramref name="pipelineMapper"/> as factory to mapping handler types to pipeline factories.
        /// </summary>
        /// <param name="routeTable">Route table to register routes to.</param>
        /// <param name="pipelineMapper">Mapper between handler type and its pipeline factory.</param>
        public ConventionRouteLoader(IRouteTable routeTable, Func<Type, IPipelineFactory> pipelineMapper)
        {
            Guard.NotNull(routeTable, "routeTable");
            Guard.NotNull(pipelineMapper, "pipelineMapper");
            RouteTable = routeTable;
            PipelineMapper = pipelineMapper;
            Convensions = new List<IConvention>();
        }

        /// <summary>
        /// Adds handlers from <paramref name="assembly"/>.
        /// </summary>
        /// <param name="assembly">Assembly where to look for handler types.</param>
        public virtual void AddAssembly(Assembly assembly)
        {
            AddTypes(assembly.GetTypes());
        }

        /// <summary>
        /// Enumerates <paramref name="typeDefinitions"/> and adds handler types.
        /// </summary>
        /// <param name="typeDefinitions">Enumeration of types to look for handler types.</param>
        protected virtual void AddTypes(IEnumerable<Type> typeDefinitions)
        {
            foreach (Type typeDefinition in typeDefinitions)
                AddType(typeDefinition);
        }

        /// <summary>
        /// Test registered conventions whether <paramref name="typeDefinition"/> can be registered as handler.
        /// </summary>
        /// <param name="typeDefinition">Possible handler type.</param>
        protected void AddType(Type typeDefinition)
        {
            foreach (IConvention convention in Convensions)
            {
                IRoute route;
                if(convention.TryGetRoute(typeDefinition, out route))
                {
                    RouteTable.Add(route, PipelineMapper(typeDefinition));
                    return;
                }
            }
        }
    }
}
