using Neptuo.Activators;
using Neptuo.FileSystems.Features;
using Neptuo.Models.Features;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems
{
    /// <summary>
    /// Implementation of factory for file streams.
    /// Takes single file and provides its content.
    /// </summary>
    public class FileContentFactory : IFactory<Stream>
    {
        private readonly IFile file;

        /// <summary>
        /// Creates new instance that reads content from <paramref name="file"/>.
        /// </summary>
        /// <param name="file"></param>
        public FileContentFactory(IFile file)
        {
            Ensure.NotNull(file, "file");
            this.file = file;
        }

        public Stream Create()
        {
            return file.With<IFileContentReader>().GetContentAsStream();
        }
    }
}
