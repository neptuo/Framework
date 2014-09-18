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
        private IDirectory parent;
        private long? fileSize;

        public string Name { get; private set; }
        public string Extension { get; private set; }
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

        public long FileSize
        {
            get
            {
                if (fileSize == null)
                    fileSize = GetFileSize(FullPath);

                return fileSize.Value;
            }
        }

        /// <summary>
        /// Creates new instance that points to <paramref name="fullPath"/>.
        /// </summary>
        /// <param name="fullPath">Standard file system path to file.</param>
        internal StaticFile(string fullPath)
        {
            SetFileRelatedProperties(fullPath);
        }

        /// <summary>
        /// Creates new instance that points to <paramref name="fullPath"/> and uses <paramref name="parent"/> as its parent.
        /// </summary>
        /// <param name="parent">Virtual parent directory.</param>
        /// <param name="fullPath">Standard file system path to file.</param>
        internal StaticFile(IDirectory parent, string fullPath)
        {
            Guard.NotNull(parent, "parent");
            Guard.NotNullOrEmpty(fullPath, "fullPath");
            Parent = parent;
            SetFileRelatedProperties(fullPath);
        }

        /// <summary>
        /// Sets file related properties from <paramref name="fullPath"/>.
        /// </summary>
        /// <param name="fullPath">Standard file system path to file.</param>
        private void SetFileRelatedProperties(string fullPath)
        {
            if (!File.Exists(fullPath))
                throw new ArgumentException("Provided path must be existing file.", "fullPath");

            FullPath = fullPath;
            Name = Path.GetFileNameWithoutExtension(fullPath);
            Extension = Path.GetExtension(fullPath);
        }

        /// <summary>
        /// Helper to get length of file.
        /// </summary>
        /// <param name="fullPath">Standard file system path to file.</param>
        /// <returns>Length of file.</returns>
        private static long GetFileSize(string fullPath)
        {
            return new FileInfo(fullPath).Length;
        }

        /// <summary>
        /// Helper to create parent directory virtual instance.
        /// </summary>
        /// <param name="fullPath">Standard file system path to file.</param>
        /// <returns>Wrapped parent directory.</returns>
        private static IDirectory GetParentDirectoryFromFullPath(string fullPath)
        {
            return new StaticDirectory(Path.GetDirectoryName(fullPath));
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

        public void SetContent(string fileContent)
        {
            File.WriteAllText(FullPath, fileContent);
        }

        public void SetContentFromByteArray(byte[] fileContent)
        {
            File.WriteAllBytes(FullPath, fileContent);
        }

        public void SetContentFromStream(Stream fileContent)
        {
            using (FileStream fileStream = new FileStream(FullPath, FileMode.OpenOrCreate))
            {
                fileContent.CopyTo(fileStream);
            }
        }
    }
}
