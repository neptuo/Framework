using Neptuo.IO;
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
    /// Enumerates child files of local file system directory.
    /// </summary>
    public class LocalFileEnumerator : IFileEnumerator
    {
        private readonly string parentDirectory;

        /// <summary>
        /// Creates new instance that enumerates child files of <paramref name="parentDirectory"/>.
        /// </summary>
        /// <param name="parentDirectory">Directory to enumerate children of.</param>
        public LocalFileEnumerator(string parentDirectory)
        {
            Ensure.Condition.DirectoryExists(parentDirectory, "parentDirectory");
            this.parentDirectory = parentDirectory;
        }

        public IEnumerator<IFile> GetEnumerator()
        {
            return new ArrayEnumerator<IFile>(Directory.GetFiles(parentDirectory), file => new LocalFile(file));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
