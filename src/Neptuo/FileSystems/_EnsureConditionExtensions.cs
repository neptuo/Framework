using Neptuo.Exceptions.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems
{
    /// <summary>
    /// IO extensions for <see cref="Ensure.Condition"/>.
    /// </summary>
    public static class _EnsureConditionExtensions
    {
        /// <summary>
        /// Throws <see cref="ArgumentOutOfRangeException"/> when <paramref name="directoryPath"/> is not existing directory.
        /// Also when <paramref name="directoryPath"/> is <c>null</c> or <see cref="String.Empty"/>, argument exception are thrown.
        /// </summary>
        /// <param name="condition">Ensure condition helper.</param>
        /// <param name="directoryPath">Directory path to test.</param>
        /// <param name="argumentName">Argument name.</param>
        public static void DirectoryExists(this EnsureConditionHelper condition, string directoryPath, string argumentName)
        {
            Ensure.NotNull(condition, "condition");
            Ensure.NotNullOrEmpty(directoryPath, "directoryPath");

            if (!Directory.Exists(directoryPath))
                throw Ensure.Exception.ArgumentOutOfRange(argumentName, "'{0}' is not existing directory.", directoryPath);
        }

        /// <summary>
        /// Throws <see cref="ArgumentOutOfRangeException"/> when <paramref name="filePath"/> is not existing file.
        /// Also when <paramref name="filePath"/> is <c>null</c> or <see cref="String.Empty"/>, argument exception are thrown.
        /// </summary>
        /// <param name="condition">Ensure condition helper.</param>
        /// <param name="filePath">File path to test.</param>
        /// <param name="argumentName">Argument name.</param>
        public static void FileExists(this EnsureConditionHelper condition, string filePath, string argumentName)
        {
            Ensure.NotNull(condition, "condition");
            Ensure.NotNullOrEmpty(filePath, "filePath");

            if (!File.Exists(filePath))
                throw Ensure.Exception.ArgumentOutOfRange(argumentName, "'{0}' is not existing file.", filePath);
        }
    }
}
