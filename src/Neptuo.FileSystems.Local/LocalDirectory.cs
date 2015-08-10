using Neptuo.Activators;
using Neptuo.FileSystems.Features;
using Neptuo.FileSystems.Features.Searching;
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
    /// Virtual file system directory implemented as stadart file system directory.
    /// </summary>
    public class LocalDirectory : CollectionFeatureModel, IDirectory, IAbsolutePath, ICreatedAt, IModefiedAt, IDirectoryRenamer, IDirectoryDeleter,
        IFactory<IDirectoryCreator>, IFactory<IFileCreator>, IFactory<IAncestorEnumerator>, IFactory<IDirectoryEnumerator>, IFactory<IFileEnumerator>,
        IFactory<IFileNameSearch>, IFactory<IFilePathSearch>, IFactory<IDirectoryNameSearch>, IFactory<IDirectoryPathSearch>
    {
        public string AbsolutePath { get; protected set; }
        public string Name { get; private set; }

        public DateTime CreatedAt
        {
            get { return Directory.GetCreationTime(AbsolutePath); }
        }

        public DateTime ModifiedAt
        {
            get { return Directory.GetLastWriteTime(AbsolutePath); }
        }

        /// <summary>
        /// Creates new instance that points to the <paramref name="absolutePath"/>.
        /// </summary>
        /// <param name="absolutePath">Standard file system path to the directory.</param>
        internal LocalDirectory(string absolutePath)
        {
            Ensure.NotNullOrEmpty(absolutePath, "absolutePath");
            SetDirectoryRelatedProperties(absolutePath);

            this
                .Add<IAbsolutePath>(this)
                .AddFactory<IAncestorEnumerator>(this)
                .AddFactory<IDirectoryCreator>(this)
                .AddFactory<IFileCreator>(this)
                .AddFactory<IDirectoryEnumerator>(this)
                .AddFactory<IFileEnumerator>(this)
                .AddFactory<IFileNameSearch>(this)
                .AddFactory<IFilePathSearch>(this)
                .AddFactory<IDirectoryNameSearch>(this)
                .AddFactory<IDirectoryPathSearch>(this)
                .Add<IDirectoryRenamer>(this)
                .Add<IDirectoryDeleter>(this);
        }

        /// <summary>
        /// Sets directory related properties from <paramref name="absolutePath"/>.
        /// </summary>
        /// <param name="absolutePath">Standard file system path to directory.</param>
        private void SetDirectoryRelatedProperties(string absolutePath)
        {
            if (!Directory.Exists(absolutePath))
                throw Ensure.Exception.Argument("absolutePath", "Provided path must be existing directory.");

            AbsolutePath = absolutePath;
            Name = Path.GetFileName(absolutePath);
            if (String.IsNullOrEmpty(Name))
                Name = absolutePath;
        }

        IAncestorEnumerator IFactory<IAncestorEnumerator>.Create()
        {
            return new LocalAncestorEnumerator(AbsolutePath);
        }

        IDirectoryCreator IFactory<IDirectoryCreator>.Create()
        {
            return new LocalDirectoryCreator(AbsolutePath);
        }

        IFileCreator IFactory<IFileCreator>.Create()
        {
            return new LocalFileCreator(AbsolutePath);
        }

        IDirectoryEnumerator IFactory<IDirectoryEnumerator>.Create()
        {
            return new LocalDirectoryEnumerator(AbsolutePath);
        }

        IFileEnumerator IFactory<IFileEnumerator>.Create()
        {
            return new LocalFileEnumerator(AbsolutePath);
        }

        IFileNameSearch IFactory<IFileNameSearch>.Create()
        {
            return new LocalSearchProvider(AbsolutePath);
        }

        IFilePathSearch IFactory<IFilePathSearch>.Create()
        {
            return new LocalSearchProvider(AbsolutePath);
        }

        IDirectoryNameSearch IFactory<IDirectoryNameSearch>.Create()
        {
            return new LocalSearchProvider(AbsolutePath);
        }

        IDirectoryPathSearch IFactory<IDirectoryPathSearch>.Create()
        {
            return new LocalSearchProvider(AbsolutePath);
        }

        public void ChangeName(string directoryName)
        {
            Ensure.NotNullOrEmpty(directoryName, "directoryName");
            LocalDirectoryCreator.EnsureValidName(directoryName);
            string newPath = Path.Combine(Path.GetDirectoryName(AbsolutePath), directoryName);
            Directory.Move(AbsolutePath, newPath);
            SetDirectoryRelatedProperties(newPath);
        }

        public void Delete()
        {
            Directory.Delete(AbsolutePath, true);
        }
    }
}
