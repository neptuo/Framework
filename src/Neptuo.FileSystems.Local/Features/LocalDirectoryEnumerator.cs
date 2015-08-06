using Neptuo.FileSystems.Internals;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems.Features
{
    /// <summary>
    /// Enumerates child directories of local file system directory.
    /// </summary>
    public class LocalDirectoryEnumerator : IEnumerable<IDirectory>, IDirectoryEnumerator
    {
        private readonly string parentDirectory;

        /// <summary>
        /// Creates new instance that enumerates child directories of <paramref name="parentDirectory"/>.
        /// </summary>
        /// <param name="parentDirectory">Directory to enumerate children of.</param>
        public LocalDirectoryEnumerator(string parentDirectory)
        {
            Ensure.NotNullOrEmpty(parentDirectory, "parentDirectory");
            this.parentDirectory = parentDirectory;
        }

        public IEnumerator<IDirectory> GetEnumerator()
        {
            return new ArrayEnumerator<IDirectory>(Directory.GetDirectories(parentDirectory), directory => new LocalDirectory(directory));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
