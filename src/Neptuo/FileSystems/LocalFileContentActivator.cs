using Neptuo.Activators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems
{
    /// <summary>
    /// Implementation of activator that creates instance of stream containing content of file.
    /// Streams must be disposed by the caller.
    /// </summary>
    public class LocalFileContentActivator : IActivator<Stream>
    {
        private readonly string filePath;
        private readonly FileMode fileMode;

        /// <summary>
        /// Creates new instance that creates stream from file at <paramref name="filePath"/>.
        /// </summary>
        /// <param name="filePath">File path.</param>
        /// <param name="fileMode">File model to use when creating stream.</param>
        public LocalFileContentActivator(string filePath, FileMode fileMode)
        {
            Ensure.NotNullOrEmpty(filePath, "filePath");
            this.filePath = filePath;
            this.fileMode = fileMode;
        }

        public Stream Create()
        {
            return new FileStream(filePath, fileMode);
        }
    }
}
