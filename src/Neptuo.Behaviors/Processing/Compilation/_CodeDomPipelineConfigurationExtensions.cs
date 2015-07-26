using Neptuo.Compilers;
using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.ComponentModel.Behaviors.Processing.Compilation
{
    /// <summary>
    /// Extensions for compiler configuration for pipeline compilation.
    /// </summary>
    public static class _CodeDomPipelineConfigurationExtensions
    {
        #region BaseType

        /// <summary>
        /// Returns required pipeline base type.
        /// </summary>
        /// <param name="configuration">Compiler configuration.</param>
        /// <returns>Required pipeline base type.</returns>
        public static Type BaseType(this ICompilerConfiguration configuration)
        {
            Ensure.NotNull(configuration, "configuration");
            return configuration.Get("BaseType", typeof(DefaultPipelineBase<>));
        }

        /// <summary>
        /// Sets equired pipeline base type.
        /// </summary>
        /// <param name="configuration">Compiler configuration.</param>
        /// <param name="baseType">Required pipeline base type.</param>
        /// <returns>Self (for fluency).</returns>
        public static ICompilerConfiguration BaseType(this ICompilerConfiguration configuration, Type baseType)
        {
            Ensure.NotNull(configuration, "configuration");
            configuration.Set("BaseType", baseType);
            return configuration;
        }

        #endregion

        /// <summary>
        /// Returns registry for behavior instance generators.
        /// </summary>
        /// <param name="configuration">Compiler configuration.</param>
        /// <returns>Registry for behavior instance generators.</returns>
        public static CodeDomBehaviorInstanceGeneratorCollection BehaviorInstance(this ICompilerConfiguration configuration)
        {
            Ensure.NotNull(configuration, "configuration");
            CodeDomBehaviorInstanceGeneratorCollection registry;
            if (!configuration.TryGet("BehaviorInstance", out registry))
                configuration.Set("BehaviorInstance", registry = new CodeDomBehaviorInstanceGeneratorCollection());

            return registry;
        }
    }
}
