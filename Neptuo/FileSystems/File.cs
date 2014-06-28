using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IoFile = System.IO.File;

namespace Neptuo.FileSystems
{
    /// <summary>
    /// Virtual file system file implemented as stadart file system file.
    /// </summary>
    public class File : IFile
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
        public File(string fullPath)
        {
            Guard.NotNullOrEmpty(fullPath, "fullPath");
            if (!IoFile.Exists(fullPath))
                throw new ArgumentException("Provided path must be existing file.", "fullPath");

            FullPath = fullPath;
            Name = Path.GetFileNameWithoutExtension(fullPath);
            Extension = Path.GetExtension(fullPath);
        }

        public string GetContent()
        {
            return IoFile.ReadAllText(FullPath);
        }

        public byte[] GetContentAsByteArray()
        {
            return IoFile.ReadAllBytes(FullPath);
        }

        public Stream GetContentAsStream()
        {
            return new FileStream(FullPath, FileMode.Open);
        }
    }
}
