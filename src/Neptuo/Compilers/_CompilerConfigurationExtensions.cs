using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Compilers
{
    /// <summary>
    /// Common extensions for <see cref="ICompilerConfiguration"/>.
    /// </summary>
    public static class _CompilerConfigurationExtensions
    {
        /// <summary>
        /// Returns collection of references.
        /// </summary>
        /// <param name="configuration">Compiler configuration.</param>
        /// <returns>Collection of references.</returns>
        public static CompilerReferenceCollection References(this ICompilerConfiguration configuration)
        {
            Ensure.NotNull(configuration, "configuration");

            CompilerReferenceCollection references;
            if(!configuration.TryGet("References", out references))
                configuration.Set("References", references = new CompilerReferenceCollection());

            return references;
        }

        #region IsDebugMode

        /// <summary>
        /// Returns whether debug mode is enabled.
        /// </summary>
        /// <param name="configuration">Compiler configuration.</param>
        /// <rereturns>Whether debug mode is enabled.</rereturns>
        public static bool IsDebugMode(this ICompilerConfiguration configuration)
        {
            Ensure.NotNull(configuration, "configuration");
            return configuration.GetBool("IsDebugMode", false);
        }

        /// <summary>
        /// Sets whether debug mode is enabled.
        /// </summary>
        /// <param name="configuration">Compiler configuration.</param>
        /// <param name="isDebugMode">New value for debug mode.</param>
        /// <returns>Self (for fluency).</returns>
        public static ICompilerConfiguration IsDebugMode(this ICompilerConfiguration configuration, bool isDebugMode)
        {
            Ensure.NotNull(configuration, "configuration");
            configuration.Set("IsDebugMode", isDebugMode);
            return configuration;
        }

        #endregion

        #region TempDirectory

        /// <summary>
        /// Returns path to temp directory.
        /// </summary>
        /// <param name="configuration">Compiler configuration.</param>
        /// <returns>Path to temp directory.</returns>
        public static string TempDirectory(this ICompilerConfiguration configuration)
        {
            Ensure.NotNull(configuration, "configuration");
            return configuration.GetString("TempDirectory", Path.GetTempPath());
        }

        /// <summary>
        /// Sets path to temp directory.
        /// </summary>
        /// <param name="configuration">Compiler configuration.</param>
        /// <param name="tempDirectory">Path to temp directory.</param>
        /// <returns>Self (for fluency).</returns>
        public static ICompilerConfiguration TempDirectory(this ICompilerConfiguration configuration, string tempDirectory)
        {
            Ensure.NotNull(configuration, "configuration");
            configuration.Set("TempDirectory", tempDirectory);
            return configuration;
        }

        #endregion


    }
}
