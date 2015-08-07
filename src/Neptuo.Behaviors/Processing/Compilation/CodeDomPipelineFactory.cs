using Neptuo.Activators;
using Neptuo.Behaviors.Processing.Compilation.Internals;
using Neptuo.Behaviors.Providers;
using Neptuo.Compilers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Behaviors.Processing.Compilation
{
    /// <summary>
    /// Factory for generating compiled class based implementation of <see cref="IPipeline{T}"/>.
    /// </summary>
    public class CodeDomPipelineFactory<T> : IFactory<IPipeline<T>>
    {
        private readonly IBehaviorProvider behaviorProvider;
        private readonly ICompilerConfiguration compilerConfiguration;
        private Type pipelineType;

        /// <summary>
        /// Creates new instance based on <paramref name="configuration"/>.
        /// </summary>
        /// <param name="configuration">Pipeline generator configuration.</param>
        public CodeDomPipelineFactory(CodeDomPipelineConfiguration configuration)
        {
            Ensure.NotNull(configuration, "configuration");
            behaviorProvider = configuration.BehaviorProvider;
            compilerConfiguration = configuration.CompilerConfiguration;
        }

        private void EnsureGeneratedPipeline()
        {
            if (pipelineType == null)
                GeneratePipeline();
        }

        private void GeneratePipeline()
        {
            CodeDomPipelineGenerator generator = new CodeDomPipelineGenerator(typeof(T), behaviorProvider, compilerConfiguration);
            pipelineType = generator.GeneratePipeline();
        }

        public IPipeline<T> Create()
        {
            EnsureGeneratedPipeline();
            return (IPipeline<T>)Activator.CreateInstance(pipelineType);
        }
    }
}
