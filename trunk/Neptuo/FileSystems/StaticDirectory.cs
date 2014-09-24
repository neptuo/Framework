using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems
{
    /// <summary>
    /// Virtual file system directory implemented as stadart file system directory.
    /// </summary>
    public class StaticDirectory : IDirectory
    {
        private IDirectory parent;

        public string Name { get; private set; }
        public string FullPath { get; private set; }

        public IDirectory Parent
        {
            get
            {
                if (parent == null)
                    parent = GetParentDirectoryFromFullPath(FullPath);

                return parent;
            }
            private set { parent = value; }
        }

        /// <summary>
        /// Creates new instance that points to <paramref name="fullPath"/>.
        /// </summary>
        /// <param name="fullPath">Standard file system path to directory.</param>
        internal StaticDirectory(string fullPath)
        {
            Guard.NotNullOrEmpty(fullPath, "fullPath");
            SetDirectoryRelatedProperties(fullPath);
        }

        internal StaticDirectory(IDirectory parent, string fullPath)
        {
            Guard.NotNull(parent, "parent");
            Guard.NotNullOrEmpty(fullPath, "fullPath");
            Parent = parent;
            SetDirectoryRelatedProperties(fullPath);
        }

        /// <summary>
        /// Sets directory related properties from <paramref name="fullPath"/>.
        /// </summary>
        /// <param name="fullPath">Standard file system path to directory.</param>
        private void SetDirectoryRelatedProperties(string fullPath)
        {
            if (!Directory.Exists(fullPath))
                throw new ArgumentException("Provided path must be existing directory.", "fullPath");

            Name = Path.GetFileName(fullPath);
            FullPath = fullPath;
        }

        /// <summary>
        /// Helper to create parent directory virtual instance.
        /// </summary>
        /// <param name="fullPath">Standard file system path to directory.</param>
        /// <returns>Wrapped parent directory.</returns>
        private static IDirectory GetParentDirectoryFromFullPath(string fullPath)
        {
            return new StaticDirectory(Path.GetDirectoryName(fullPath));
        }

        /// <summary>
        /// Returns enumeration of directories with this as parent.
        /// </summary>
        /// <param name="paths">Enumeration of directory paths</param>
        /// <returns>Enumeration of directories with this as parent.</returns>
        private IEnumerable<IDirectory> EnumerateChildDirectories(IEnumerable<string> paths)
        {
            foreach (string path in paths)
                yield return new StaticDirectory(this, path);
        }

        /// <summary>
        /// Returns enumeration of directories without explicit parent.
        /// </summary>
        /// <param name="paths">Enumeration of directory paths</param>
        /// <returns>Enumeration of directories without explicit parent.</returns>
        private IEnumerable<IDirectory> EnumerateAllDirectories(IEnumerable<string> paths)
        {
            foreach (string path in paths)
                yield return new StaticDirectory(path);
        }

        /// <summary>
        /// Returns instance of <see cref="SearchOption"/> from <paramref name="inAllDescendants"/>.
        /// </summary>
        /// <param name="inAllDescendants">True for not only direct childs.</param>
        /// <returns>Instance of <see cref="SearchOption"/> from <paramref name="inAllDescendants"/>.</returns>
        private SearchOption GetSearchOption(bool inAllDescendants)
        {
            SearchOption searchOption = SearchOption.TopDirectoryOnly;
            if (inAllDescendants)
                searchOption = SearchOption.AllDirectories;

            return searchOption;
        }

        public IEnumerable<IDirectory> EnumerateDirectories()
        {
            return EnumerateChildDirectories(Directory.GetDirectories(FullPath));
        }

        public IEnumerable<IDirectory> FindDirectories(string searchPattern, bool inAllDescendants)
        {
            Guard.NotNullOrEmpty(searchPattern, "searchPattern");
            IEnumerable<string> paths = Directory.GetDirectories(FullPath, searchPattern, GetSearchOption(inAllDescendants));
            if (!inAllDescendants)
                return EnumerateChildDirectories(paths);

            return EnumerateAllDirectories(paths);
        }

        public IEnumerable<IFile> EnumerateFiles()
        {
            foreach (string path in Directory.GetFiles(FullPath))
                yield return new StaticFile(this, path);
        }

        public IEnumerable<IFile> FindFiles(string searchPattern, bool inAllDescendants)
        {
            Guard.NotNullOrEmpty(searchPattern, "searchPattern");
            IEnumerable<string> paths = Directory.GetFiles(FullPath, searchPattern, GetSearchOption(inAllDescendants));
            if (!inAllDescendants)
            {
                foreach (string path in paths)
                    yield return new StaticFile(this, path);
            }
            else
            {
                foreach (string path in paths)
                    yield return new StaticFile(path);
            }
        }

        public bool ContainsDirectoryName(string directoryName)
        {
            Guard.NotNullOrEmpty(directoryName, "directoryName");
            return Directory.Exists(Path.Combine(FullPath, directoryName));
        }

        public bool ContainsFileName(string fileName)
        {
            Guard.NotNullOrEmpty(fileName, "fileName");
            return File.Exists(Path.Combine(FullPath, fileName));
        }

        public Task<IDirectory> CreateDirectory(string directoryName)
        {
            Guard.NotNullOrEmpty(directoryName, "directoryName");
            DirectoryInfo newDirectory = Directory.CreateDirectory(Path.Combine(FullPath, directoryName));
            return Task.FromResult<IDirectory>(new StaticDirectory(this, newDirectory.FullName));
        }

        public Task<IFile> CreateFile(string fileName)
        {
            Guard.NotNullOrEmpty(fileName, "fileName");
            string filePath= Path.Combine(FullPath, fileName);
            File.Create(filePath).Dispose();
            return Task.FromResult<IFile>(new StaticFile(this, filePath));
        }
    }
}
