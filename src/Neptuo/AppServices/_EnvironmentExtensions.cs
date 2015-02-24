using Neptuo.ComponentModel.Behaviors;
using Neptuo.ComponentModel.Behaviors.Processing.Compilation;
using Neptuo.ComponentModel.Behaviors.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.AppServices
{
    /// <summary>
    /// Behaviors.Processing extensions for <see cref="EngineEnvironment"/>.
    /// </summary>
    public static class _EnvironmentExtensions
    {
        public class AppServiceEngineEnvironment
        {
            public EngineEnvironment Environment { get; private set; }

            public AppServiceEngineEnvironment(EngineEnvironment environment)
            {
                Guard.NotNull(environment, "environment");
                Environment = environment;
            }
        }

        /// <summary>
        /// Registers app services.
        /// </summary>
        /// <param name="environment">Engine environment.</param>
        public static AppServiceEngineEnvironment UseAppServices(this EngineEnvironment environment)
        {
            Guard.NotNull(environment, "environment");
            return new AppServiceEngineEnvironment(environment);
        }

        /// <summary>
        /// Registers singleton behaviors collection.
        /// </summary>
        /// <param name="appService">Engine environment.</param>
        /// <param name="behaviors">Behaviors collection.</param>
        /// <returns><paramref name="appService"/>.</returns>
        public static AppServiceEngineEnvironment UseBehaviors(this AppServiceEngineEnvironment appService, IBehaviorCollection behaviors)
        {
            Guard.NotNull(appService, "appService");
            appService.Environment.Use<IBehaviorCollection>(behaviors, "AppService.Behaviors");
            return appService;
        }

        /// <summary>
        /// Registers behaviors collection.
        /// </summary>
        /// <param name="appService">Engine environment.</param>
        /// <param name="providers">List of behavior providers to add.</param>
        /// <returns><paramref name="appService"/>.</returns>
        public static AppServiceEngineEnvironment UseBehaviors(this AppServiceEngineEnvironment appService, params IBehaviorProvider[] providers)
        {
            Guard.NotNull(appService, "appService");
            Guard.NotNull(appService, "environment");
            Guard.NotNull(providers, "providers");

            IBehaviorCollection collection = new BehaviorProviderCollection();
            foreach (IBehaviorProvider provider in providers)
                collection.Add(provider);

            return appService.UseBehaviors(collection);
        }

        /// <summary>
        /// Registers behaviors collection, add enpoint behaviors and invokes <paramref name="mapper"/> to map interface behaviors.
        /// </summary>
        /// <param name="appService">Engine environment.</param>
        /// <param name="mapper">Interface behavior mapper.</param>
        /// <returns><paramref name="appService"/>.</returns>
        public static AppServiceEngineEnvironment UseBehaviors(this AppServiceEngineEnvironment appService, Action<InterfaceBehaviorProvider> mapper)
        {
            Guard.NotNull(appService, "appService");
            Guard.NotNull(appService, "environment");
            Guard.NotNull(mapper, "mapper");

            InterfaceBehaviorProvider provider = new InterfaceBehaviorProvider();
            mapper(provider);
            return appService.UseBehaviors(provider);
        }

        /// <summary>
        /// Returns registration of app services.
        /// </summary>
        /// <param name="environment">Engine environment.</param>
        public static AppServiceEngineEnvironment WithAppServices(this EngineEnvironment environment)
        {
            Guard.NotNull(environment, "environment");
            return new AppServiceEngineEnvironment(environment);
        }

        /// <summary>
        /// Tries to retrieve behaviors collection.
        /// </summary>
        /// <param name="appService">Engine environment.</param>
        /// <returns>Registered behaviors collection.</returns>
        public static IBehaviorCollection WithBehaviors(this AppServiceEngineEnvironment appService)
        {
            Guard.NotNull(appService, "appService");
            return appService.Environment.With<IBehaviorCollection>("AppService.Behaviors");
        }

        /// <summary>
        /// Registers singleton code dom pipeline configuration.
        /// </summary>
        /// <param name="appService">Engine environment.</param>
        /// <param name="configuration">Code dom pipeline configuration.</param>
        /// <returns><paramref name="appService"/>.</returns>
        public static AppServiceEngineEnvironment UseCodeDomConfiguration(this AppServiceEngineEnvironment appService, CodeDomPipelineConfiguration configuration)
        {
            Guard.NotNull(appService, "appService");
            appService.Environment.Use<CodeDomPipelineConfiguration>(configuration, "AppService.CodeDomConfiguration");
            return appService;
        }

        /// <summary>
        /// Registers singleton code dom pipeline configuration.
        /// </summary>
        /// <param name="appService">Engine environment.</param>
        /// <param name="baseType">Custom base type (extending <see cref="DefaultPipelineBase{T}"/>).</param>
        /// <param name="tempDirectory">Path to temp directory.</param>
        /// <param name="binDirectories">List of bin directories to add as references.</param>
        /// <returns><paramref name="appService"/>.</returns>
        public static AppServiceEngineEnvironment UseCodeDomConfiguration(this AppServiceEngineEnvironment appService, Type baseType, string tempDirectory, params string[] binDirectories)
        {
            return appService.UseCodeDomConfiguration(new CodeDomPipelineConfiguration(baseType, tempDirectory, binDirectories));
        }

        /// <summary>
        /// Tries to retrieve code dom pipeline configuration.
        /// </summary>
        /// <param name="environment">Engine environment.</param>
        /// <returns>Registered code dom pipeline configuration.</returns>
        public static CodeDomPipelineConfiguration WithCodeDomConfiguration(this AppServiceEngineEnvironment appService)
        {
            return appService.Environment.With<CodeDomPipelineConfiguration>();
        }
    }
}
