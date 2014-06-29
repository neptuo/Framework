using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems
{
    /// <summary>
    /// Virtual file system implemented as stadart file system.
    /// </summary>
    public class StaticFileSystem : StaticDirectory, IFileSystem
    {
        public StaticFileSystem(string rootPath)
            : base(rootPath)
        {
            if (!Path.IsPathRooted(rootPath))
                throw new ArgumentException("Path to file system must be rooted.", "rootPath");
        }
    }
}
