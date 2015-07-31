using Neptuo.Behaviors.Providers;
using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Behaviors.Processing.Compilation
{
    /// <summary>
    /// Code dom generated and compiled class based implementation of <see cref="IPipeline{T}"/>.
    /// For <typeparamref name="T"/> single class is generated and compiled for better behavior instances creation.
    /// </summary>
    /// <typeparam name="T">Type of inner handler.</typeparam>
    public class CodeDomPipeline<T> : IPipeline<T>
    {
        private readonly ICodeDomBehaviorGenerator behaviorGenerator;
        private readonly IPipeline<T> generatedPipeline;

        private void EnsureGeneratedPipeline()
        {
            if (generatedPipeline == null)
                GeneratePipeline();
        }

        private void GeneratePipeline()
        {

        }

        public IPipeline<T> AddBehavior(PipelineBehaviorPosition position, IBehavior<T> behavior)
        {
            EnsureGeneratedPipeline();
            generatedPipeline.AddBehavior(position, behavior);
            return this;
        }

        public Task ExecuteAsync(T handler, IKeyValueCollection customValues)
        {
            EnsureGeneratedPipeline();
            return generatedPipeline.ExecuteAsync(handler, customValues);
        }
    }
}