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
    public class LocalFile : CollectionFeatureModel, IFile, IAbsolutePath, ICreatedAt, IModefiedAt, IFileRenamer, IFileDeleter,
        IActivator<IAncestorEnumerator>,
        IFileContentSize, IFileContentReader, IFileContentUpdater, IFileContentAppender
    {
        public string AbsolutePath { get; protected set; }
        public string Name { get; private set; }
        public string Extension { get; private set; }

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
        {
            Ensure.NotNullOrEmpty(absolutePath, "absolutePath");
            SetFileRelatedProperties(absolutePath);

            this
                .Add<IAbsolutePath>(this)
                .AddFactory<IAncestorEnumerator>(this)
                .Add<IFileContentSize>(this)
                .Add<IFileContentReader>(this)
                .Add<IFileContentUpdater>(this)
                .Add<IFileContentAppender>(this)
                .Add<IFileRenamer>(this)
                .Add<IFileDeleter>(this);
        }
        /// <summary>
        /// Sets file related properties from <paramref name="absolutePath"/>.
        /// </summary>
        /// <param name="absolutePath">Standard file system path to file.</param>
        private void SetFileRelatedProperties(string absolutePath)
        {
            if (!File.Exists(absolutePath))
                throw Ensure.Exception.Argument("fullPath", "Provided path must be existing file.");

            AbsolutePath = absolutePath;
            Name = Path.GetFileNameWithoutExtension(absolutePath);
            Extension = Path.GetExtension(absolutePath);
            if (Extension.StartsWith("."))
                Extension = Extension.Substring(1);
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

        public async Task AppendContentAsync(string fileContent)
        {
            using (TextWriter writer = File.AppendText(AbsolutePath))
                await writer.WriteAsync(fileContent);
        }

        public async Task AppendContentFromByteArrayAsync(byte[] fileContent)
        {
            using(FileStream writer = new FileStream(AbsolutePath, FileMode.Append))
                await writer.WriteAsync(fileContent, 0, fileContent.Length);
        }

        public async Task AppendContentFromStreamAsync(Stream fileContent)
        {
            using (FileStream writer = new FileStream(AbsolutePath, FileMode.Append))
                await fileContent.CopyToAsync(writer);
        }

        #endregion

        public void ChangeName(string fileName)
        {
            Ensure.NotNullOrEmpty(fileName, "fileName");
            LocalFileCreator.EnsureValidName(fileName, null);

            string newPath = Path.Combine(Path.GetDirectoryName(AbsolutePath), fileName + Path.GetExtension(AbsolutePath));
            File.Move(AbsolutePath, newPath);
            SetFileRelatedProperties(newPath);
        }

        public void ChangeExtension(string fileExtension)
        {
            LocalFileCreator.EnsureValidName(null, fileExtension);

            string newFileName = Path.GetFileNameWithoutExtension(AbsolutePath);
            if (!String.IsNullOrEmpty(fileExtension))
                newFileName += "." + fileExtension;

            string newPath = Path.Combine(Path.GetDirectoryName(AbsolutePath));
            File.Move(AbsolutePath, newPath);
            SetFileRelatedProperties(newPath);
        }

        public void Delete()
        {
            File.Delete(AbsolutePath);
        }
    }
}
