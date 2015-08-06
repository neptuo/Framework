using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems.Features
{
    /// <summary>
    /// Implementation of <see cref="IFileSystemConstant"/> for local file system.
    /// </summary>
    public class LocalFileSystemConstant : IFileSystemConstant
    {
        public char PathSeparator
        {
            get { return Path.PathSeparator; }
        }

        public IEnumerable<char> InvalidDirectoryNameChars
        {
            get { return Path.GetInvalidPathChars(); }
        }

        public IEnumerable<char> InvalidFileNameChars
        {
            get { return Path.GetInvalidFileNameChars(); }
        }
    }
}
