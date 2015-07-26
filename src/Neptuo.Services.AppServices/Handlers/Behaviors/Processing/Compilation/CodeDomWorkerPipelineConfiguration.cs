using Neptuo.Compilers;
using Neptuo.ComponentModel.Behaviors;
using Neptuo.ComponentModel.Behaviors.Processing.Compilation;
using Neptuo.ComponentModel.Behaviors.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.AppServices.Handlers.Behaviors.Processing.Compilation
{
    /// <summary>
    /// Configuration of AppServices pipeline.
    /// </summary>
    public class CodeDomWorkerPipelineConfiguration : CodeDomPipelineConfiguration
    {
        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="behaviors">Behaviors provider.</param>
        /// <param name="compilerConfiguration">Pipeline compiler configuration.</param>
        public CodeDomWorkerPipelineConfiguration(IBehaviorProvider behaviors, ICompilerConfiguration compilerConfiguration)
            : base(behaviors, compilerConfiguration)
        { }
    }
}
