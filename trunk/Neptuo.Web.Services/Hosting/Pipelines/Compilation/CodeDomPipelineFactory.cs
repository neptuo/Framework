using Neptuo.Web.Services.Hosting.Behaviors;
using Neptuo.Web.Services.Hosting.Http;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Services.Hosting.Pipelines.Compilation
{
    /// <summary>
    /// Generates pipeline using <see cref="System.CodeDom"/>.
    /// </summary>
    public class CodeDomPipelineFactory : IPipelineFactory
    {
        /// <summary>
        /// Handler type.
        /// </summary>
        private readonly Type handlerType;

        /// <summary>
        /// Function that creates instance of pipeline.
        /// </summary>
        private Func<IPipeline> generatedFactory;

        /// <summary>
        /// Behavior collection.
        /// </summary>
        private readonly IBehaviorCollection behaviorCollection;

        /// <summary>
        /// Generator configuration.
        /// </summary>
        private readonly CodeDomPipelineConfiguration configuration;

        /// <summary>
        /// Creates new instance for handler of type <paramref name="handlerType"/>.
        /// </summary>
        /// <param name="handlerType">Handler type.</param>
        /// <param name="behaviorCollection">Behavior collection.</param>
        /// <param name="configuration">Generator configuration.</param>
        public CodeDomPipelineFactory(Type handlerType, IBehaviorCollection behaviorCollection, CodeDomPipelineConfiguration configuration)
        {
            Guard.NotNull(handlerType, "handlerType");
            Guard.NotNull(behaviorCollection, "behaviorCollection");
            Guard.NotNull(configuration, "configuration");
            this.handlerType = handlerType;
            this.behaviorCollection = behaviorCollection;
            this.configuration = configuration;
        }

        /// <summary>
        /// Creates instance of compiled pipeline.
        /// </summary>
        /// <returns>Instance of pipeline for handler type.</returns>
        public IPipeline Create()
        {
            EnsurePipelineFactory();
            return generatedFactory();
        }

        /// <summary>
        /// Ensures that pipeline factory is created.
        /// </summary>
        private void EnsurePipelineFactory()
        {
            if (generatedFactory == null)
                GeneratePipelineFactory();
        }

        /// <summary>
        /// Creates pipeline factory using <see cref="CodeDomPipelineGenerator"/>.
        /// </summary>
        private void GeneratePipelineFactory()
        {
            CodeDomPipelineGenerator generator = new CodeDomPipelineGenerator(handlerType, behaviorCollection, configuration);
            Type pipelineType = generator.GeneratePipeline();

            generatedFactory = () => (IPipeline)Activator.CreateInstance(pipelineType);
        }
    }
}
