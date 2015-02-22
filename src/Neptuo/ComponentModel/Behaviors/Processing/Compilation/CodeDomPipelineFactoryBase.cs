using Neptuo.ComponentModel.Behaviors;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.ComponentModel.Behaviors.Processing.Compilation
{
    /// <summary>
    /// Generates pipeline using <see cref="System.CodeDom"/>.
    /// </summary>
    /// <typeparam name="T">Base type (or required interface) of generated type.</typeparam>
    public abstract class CodeDomPipelineFactoryBase<T>
    {
        /// <summary>
        /// Handler type.
        /// </summary>
        private readonly Type handlerType;

        /// <summary>
        /// Function that creates instance of pipeline.
        /// </summary>
        private Func<T> generatedFactory;

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
        public CodeDomPipelineFactoryBase(Type handlerType)
            : this(handlerType, Engine.Environment.WithBehaviors(), Engine.Environment.WithCodeDomConfiguration())
        { }

        /// <summary>
        /// Creates new instance for handler of type <paramref name="handlerType"/>.
        /// </summary>
        /// <param name="handlerType">Handler type.</param>
        /// <param name="behaviorCollection">Behavior collection.</param>
        /// <param name="configuration">Generator configuration.</param>
        public CodeDomPipelineFactoryBase(Type handlerType, IBehaviorCollection behaviorCollection, CodeDomPipelineConfiguration configuration)
        {
            Guard.NotNull(handlerType, "handlerType");
            Guard.NotNull(behaviorCollection, "behaviorCollection");
            Guard.NotNull(configuration, "configuration");
            this.handlerType = handlerType;
            this.behaviorCollection = behaviorCollection;
            this.configuration = configuration;
        }

        protected T CreateInstance()

        public async Task<IHttpResponse> TryHandleAsync(IHttpRequest httpRequest)
        {
            EnsurePipelineFactory();
            IRequestHandler pipeline = generatedFactory();
            return await pipeline.TryHandleAsync(httpRequest);
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

            generatedFactory = () => (T)Activator.CreateInstance(pipelineType);
        }
    }
}
