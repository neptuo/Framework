using Neptuo.DomainModels;
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
    public class LocalFile : IFile
    {
        private IDirectory parent;
        private readonly LocalFileSystemKey key;
        private long? fileSize;

        public string Name { get; private set; }
        public string Extension { get; private set; }
        public string FullPath { get; private set; }

        public IKey Key
        {
            get { return key; }
        }

        public LocalFileSystemKey LocalKey
        {
            get { return key; }
        }

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
        /// Creates new instance that points to the <paramref name="fullPath"/>.
        /// </summary>
        /// <param name="fullPath">Standard file system path to file.</param>
        internal LocalFile(string fullPath)
        {
            Ensure.NotNullOrEmpty(fullPath, "fullPath");
            key = LocalFileSystemKey.Create(fullPath, "LocalFile");
            SetFileRelatedProperties(fullPath);
        }

        /// <summary>
        /// Creates new instance that points to the <paramref name="fullPath"/> and uses <paramref name="parent"/> as its parent.
        /// </summary>
        /// <param name="parent">Virtual parent directory.</param>
        /// <param name="fullPath">Standard file system path to the file.</param>
        internal LocalFile(IDirectory parent, string fullPath)
            : this(fullPath)
        {
            Ensure.NotNull(parent, "parent");
            Parent = parent;
        }

        /// <summary>
        /// Sets file related properties from <paramref name="fullPath"/>.
        /// </summary>
        /// <param name="fullPath">Standard file system path to file.</param>
        private void SetFileRelatedProperties(string fullPath)
        {
            if (!File.Exists(fullPath))
                throw Ensure.Exception.Argument("fullPath", "Provided path must be existing file.");

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
            return new LocalDirectory(Path.GetDirectoryName(fullPath));
        }


        public Task<string> GetContentAsync()
        {
            return Task.FromResult(File.ReadAllText(FullPath));
        }

        public Task<byte[]> GetContentAsByteArrayAsync()
        {
            return Task.FromResult(File.ReadAllBytes(FullPath));
        }

        public Task<Stream> GetContentAsStreamAsync()
        {
            return Task.FromResult<Stream>(new FileStream(FullPath, FileMode.Open));
        }

        public Task SetContentAsync(string fileContent)
        {
            File.WriteAllText(FullPath, fileContent);
            return Task.FromResult(true);
        }

        public Task SetContentFromByteArrayAsync(byte[] fileContent)
        {
            using (FileStream fileStream = new FileStream(FullPath, FileMode.OpenOrCreate))
            {
                return fileStream.WriteAsync(fileContent, 0, fileContent.Length);
            }
        }

        public Task SetContentFromStreamAsync(Stream fileContent)
        {
            using (FileStream fileStream = new FileStream(FullPath, FileMode.OpenOrCreate))
            {
                return fileContent.CopyToAsync(fileStream);
            }
        }
    }
}
