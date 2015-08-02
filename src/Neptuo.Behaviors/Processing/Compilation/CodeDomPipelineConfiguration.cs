using Neptuo.Compilers;
using Neptuo.Behaviors.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Behaviors.Processing.Compilation
{
    /// <summary>
    /// Configuration of <see cref="CodeDomPipelineFactory{T}"/>.
    /// </summary>
    public class CodeDomPipelineConfiguration
    {
        /// <summary>
        /// Collection of behaviors.
        /// </summary>
        public IBehaviorProvider BehaviorProvider { get; private set; }

        /// <summary>
        /// Pipeline compiler configuration.
        /// </summary>
        public ICompilerConfiguration CompilerConfiguration { get; private set; }

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="behaviorProvider">Collection of behaviors.</param>
        /// <param name="compilerConfiguration">Pipeline compiler configuration.</param>
        public CodeDomPipelineConfiguration(IBehaviorProvider behaviorProvider, ICompilerConfiguration compilerConfiguration)
        {
            Ensure.NotNull(behaviorProvider, "behaviorProvider");
            Ensure.NotNull(compilerConfiguration, "compilerConfiguration");
            BehaviorProvider = behaviorProvider;
            CompilerConfiguration = compilerConfiguration;
        }
    }
}
