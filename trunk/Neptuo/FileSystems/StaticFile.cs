using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems
{
    /// <summary>
    /// Virtual file system file implemented as stadart file system file.
    /// </summary>
    public class StaticFile : IFile
    {
        public string Name { get; private set; }
        public string Extension { get; private set; }
        public string FullPath { get; private set; }

        public IDirectory Parent
        {
            get { throw new NotImplementedException(); }
        }

        public long FileSize
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Creates new instance that points to <paramref name="fullPath"/>.
        /// </summary>
        /// <param name="fullPath">Standard file system path to file.</param>
        public StaticFile(string fullPath)
        {
            Guard.NotNullOrEmpty(fullPath, "fullPath");
            if (!File.Exists(fullPath))
                throw new ArgumentException("Provided path must be existing file.", "fullPath");

            FullPath = fullPath;
            Name = Path.GetFileNameWithoutExtension(fullPath);
            Extension = Path.GetExtension(fullPath);
        }

        public string GetContent()
        {
            return File.ReadAllText(FullPath);
        }

        public byte[] GetContentAsByteArray()
        {
            return File.ReadAllBytes(FullPath);
        }

        public Stream GetContentAsStream()
        {
            return new FileStream(FullPath, FileMode.Open);
        }
    }
}
