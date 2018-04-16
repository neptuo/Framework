using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Compilers
{
    /// <summary>
    /// Collection of compiler references.
    /// </summary>
    public class CompilerReferenceCollection : ICloneable<CompilerReferenceCollection>
    {
        /// <summary>
        /// List of referenced assemblies.
        /// </summary>
        private readonly List<string> assemblies = new List<string>();

        /// <summary>
        /// List of referenced directories.
        /// </summary>
        private readonly List<string> directories = new List<string>();

        /// <summary>
        /// Enumerates referenced assemblies.
        /// </summary>
        public IEnumerable<string> Assemblies
        {
            get { return assemblies; }
        }

        /// <summary>
        /// Enumerates referenced directories.
        /// </summary>
        public IEnumerable<string> Directories
        {
            get { return directories; }
        }

        /// <summary>
        /// Creates empty collection.
        /// </summary>
        public CompilerReferenceCollection()
        { }

        /// <summary>
        /// Creates collection with references copied from <paramref name="assemblies"/> and <paramref name="directories"/>.
        /// </summary>
        /// <param name="assemblies">Enumeration of referenced assemblies.</param>
        /// <param name="directories">Enumeration of referenced directories.</param>
        public CompilerReferenceCollection(IEnumerable<string> assemblies, IEnumerable<string> directories)
        {
            Ensure.NotNull(assemblies, "assemblies");
            Ensure.NotNull(directories, "directories");
            this.assemblies.AddRange(assemblies);
            this.directories.AddRange(directories);
        }

        public CompilerReferenceCollection AddAssembly(string assemblyFile)
        {
            Ensure.NotNullOrEmpty(assemblyFile, "assemblyFile");

            // Throw exception when file (with path - relative or absolute) doesn't exist.
            // If assemblyFile is only file name, it could be framework reference.
            if (Path.GetFileName(assemblyFile) != assemblyFile && !File.Exists(assemblyFile))
                throw Ensure.Exception.ArgumentOutOfRange("assemblyFile", "Path '{0}' must point to an existing assembly file.", assemblyFile);

            assemblies.Add(assemblyFile);
            return this;
        }

        public CompilerReferenceCollection AddDirectory(string directoryPath)
        {
            Ensure.NotNullOrEmpty(directoryPath, "directoryPath");
            if(!Directory.Exists(directoryPath))
                throw Ensure.Exception.ArgumentOutOfRange("directoryPath", "Path '{0}' must point to an existing directory.", directoryPath);

            directories.Add(directoryPath);
            return this;
        }

        public CompilerReferenceCollection Clone()
        {
            return new CompilerReferenceCollection(Assemblies, Directories);
        }
    }
}
