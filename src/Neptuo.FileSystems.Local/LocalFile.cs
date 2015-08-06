using Neptuo.Activators;
using Neptuo.FileSystems.Features;
using Neptuo.FileSystems.Features.Timestamps;
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
    /// Virtual file system file implemented as stadart file system file.
    /// </summary>
    public class LocalFile : CollectionFeatureModel, IFile, IAbsolutePath, ICreatedAt, IModefiedAt, 
        IActivator<IAncestorEnumerator>,
        IFileContentSize, IFileContentReader, IFileContentUpdater, IFileContentAppender
    {
        public string Name { get; private set; }
        public string Extension { get; private set; }
        public string AbsolutePath { get; private set; }

        public DateTime CreatedAt
        {
            get { return File.GetCreationTime(AbsolutePath); }
        }

        public DateTime ModifiedAt
        {
            get { return File.GetLastWriteTime(AbsolutePath); }
        }

        public long FileSize
        {
            get { return new FileInfo(AbsolutePath).Length; }
        }

        /// <summary>
        /// Creates new instance that points to the <paramref name="absolutePath"/>.
        /// </summary>
        /// <param name="absolutePath">Standard file system path to file.</param>
        internal LocalFile(string absolutePath)
            : base(true)
        {
            Ensure.NotNullOrEmpty(absolutePath, "fullPath");
            SetFileRelatedProperties(absolutePath);

            this
                .Add<IAbsolutePath>(this)
                .AddFactory<IAncestorEnumerator>(this)
                .Add<IFileContentSize>(this)
                .Add<IFileContentReader>(this)
                .Add<IFileContentUpdater>(this)
                .Add<IFileContentAppender>(this);
        }

        /// <summary>
        /// Sets file related properties from <paramref name="fullPath"/>.
        /// </summary>
        /// <param name="fullPath">Standard file system path to file.</param>
        private void SetFileRelatedProperties(string fullPath)
        {
            if (!File.Exists(fullPath))
                throw Ensure.Exception.Argument("fullPath", "Provided path must be existing file.");

            AbsolutePath = fullPath;
            Name = Path.GetFileNameWithoutExtension(fullPath);
            Extension = Path.GetExtension(fullPath);
        }

        IAncestorEnumerator IActivator<IAncestorEnumerator>.Create()
        {
            return new LocalAncestorEnumerator(AbsolutePath);
        }

        #region IFileContentReader

        public Task<string> GetContentAsync()
        {
            return Task.FromResult(File.ReadAllText(AbsolutePath));
        }

        public Task<byte[]> GetContentAsByteArrayAsync()
        {
            return Task.FromResult(File.ReadAllBytes(AbsolutePath));
        }

        public Task<Stream> GetContentAsStreamAsync()
        {
            return Task.FromResult<Stream>(new FileStream(AbsolutePath, FileMode.Open));
        }

        #endregion

        #region IFileContentUpdater

        public Task SetContentAsync(string fileContent)
        {
            File.WriteAllText(AbsolutePath, fileContent);
            return Task.FromResult(true);
        }

        public Task SetContentFromByteArrayAsync(byte[] fileContent)
        {
            using (FileStream fileStream = new FileStream(AbsolutePath, FileMode.OpenOrCreate))
            {
                return fileStream.WriteAsync(fileContent, 0, fileContent.Length);
            }
        }

        public Task SetContentFromStreamAsync(Stream fileContent)
        {
            using (FileStream fileStream = new FileStream(AbsolutePath, FileMode.OpenOrCreate))
            {
                return fileContent.CopyToAsync(fileStream);
            }
        }

        #endregion

        #region IFileContentAppender

        public Task AppendContentAsync(string fileContent)
        {
            throw new NotImplementedException();
        }

        public Task AppendContentFromByteArrayAsync(byte[] fileContent)
        {
            throw new NotImplementedException();
        }

        public Task AppendContentFromStreamAsync(Stream fileContent)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
