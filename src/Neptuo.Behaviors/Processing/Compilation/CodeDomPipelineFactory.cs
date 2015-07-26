using Neptuo.Activators;
using Neptuo.Compilers;
using Neptuo.Behaviors;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Behaviors.Processing.Compilation
{
    /// <summary>
    /// Generates pipeline using <see cref="System.CodeDom"/>.
    /// </summary>
    /// <typeparam name="T">Base type (or required interface) of generated type.</typeparam>
    public class CodeDomPipelineFactory<T> : IActivator<T>
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
        /// Pipeline configuration.
        /// </summary>
        private readonly CodeDomPipelineConfiguration configuration;

        /// <summary>
        /// Creates new instance for handler of type <paramref name="handlerType"/>.
        /// </summary>
        /// <param name="handlerType">Handler type.</param>
        /// <param name="behaviorCollection">Behavior collection.</param>
        /// <param name="configuration">Generator configuration.</param>
        public CodeDomPipelineFactory(Type handlerType, CodeDomPipelineConfiguration configuration)
        {
            Ensure.NotNull(handlerType, "handlerType");
            Ensure.NotNull(configuration, "configuration");
            this.handlerType = handlerType;
            this.configuration = configuration;
        }

        /// <summary>
        /// Cretes instance of generated pipeline.
        /// </summary>
        /// <returns>Instance of generated pipeline.</returns>
        public T Create()
        {
            EnsurePipelineFactory();
            T pipeline = generatedFactory();
            return pipeline;
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
            CodeDomPipelineGenerator generator = new CodeDomPipelineGenerator(handlerType, configuration.Behaviors, configuration.CompilerConfiguration);
            Type pipelineType = generator.GeneratePipeline();

            generatedFactory = () => (T)Activator.CreateInstance(pipelineType);
        }
    }
}
