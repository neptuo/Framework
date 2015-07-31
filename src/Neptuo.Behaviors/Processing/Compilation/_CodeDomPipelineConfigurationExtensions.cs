using Neptuo.Compilers;
using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Behaviors.Processing.Compilation
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

        #region BehaviorGenerator

        /// <summary>
        /// Returns behavior instance generator.
        /// </summary>
        /// <param name="configuration">Compiler configuration.</param>
        /// <param name="defaultValue">Fallback value, when <paramref name="configuration"/> doesn't contain generator.</param>
        /// <returns>Behavior instance generator.</returns>
        public static ICodeDomBehaviorGenerator GetBehaviorGenerator(this ICompilerConfiguration configuration, ICodeDomBehaviorGenerator defaultValue)
        {
            Ensure.NotNull(configuration, "configuration");
            return configuration.Get<ICodeDomBehaviorGenerator>("BehaviorGenerator", defaultValue);
        }

        /// <summary>
        /// Sets behavior instance generator.
        /// </summary>
        /// <param name="configuration">Compiler configuration.</param>
        /// <param name="generator">Behavior instance generator.</param>
        /// <returns>Self (for fluency).</returns>
        public static ICompilerConfiguration AddBehaviorGenerator(this ICompilerConfiguration configuration, ICodeDomBehaviorGenerator generator)
        {
            Ensure.NotNull(configuration, "configuration");
            configuration.Set("BehaviorGenerator", generator);
            return configuration;
        }

        #endregion
    }
}
