using Neptuo.Compilers;
using Neptuo.ComponentModel.Behaviors.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.ComponentModel.Behaviors.Processing.Compilation
{
    /// <summary>
    /// Configuration of <see cref="CodeDomPipelineFactory{T}"/>.
    /// </summary>
    public class CodeDomPipelineConfiguration
    {
        /// <summary>
        /// Collection of behaviors.
        /// </summary>
        public IBehaviorProvider Behaviors { get; private set; }

        /// <summary>
        /// Pipeline compiler configuration.
        /// </summary>
        public ICompilerConfiguration CompilerConfiguration { get; private set; }

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="behaviors">Collection of behaviors.</param>
        /// <param name="compilerConfiguration">Pipeline compiler configuration.</param>
        public CodeDomPipelineConfiguration(IBehaviorProvider behaviors, ICompilerConfiguration compilerConfiguration)
        {
            Ensure.NotNull(behaviors, "behaviors");
            Ensure.NotNull(compilerConfiguration, "compilerConfiguration");
            Behaviors = behaviors;
            CompilerConfiguration = compilerConfiguration;
        }
    }
}
