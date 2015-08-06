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
    public class LocalDirectory : LocalItemBase, IDirectory, IAbsolutePath, ICreatedAt, IModefiedAt,
        IActivator<IDirectoryCreator>, IActivator<IFileCreator>, IActivator<IAncestorEnumerator>, IActivator<IDirectoryEnumerator>, IActivator<IFileEnumerator>,
        IActivator<IFileNameSearch>, IActivator<IFilePathSearch>, IActivator<IDirectoryNameSearch>, IActivator<IDirectoryPathSearch>
    {
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
            : base(absolutePath)
        {
            Ensure.NotNullOrEmpty(absolutePath, "absolutePath");
            SetDirectoryRelatedProperties(absolutePath);

            this
                .AddFactory<IDirectoryCreator>(this)
                .AddFactory<IFileCreator>(this)
                .AddFactory<IDirectoryEnumerator>(this)
                .AddFactory<IFileEnumerator>(this)
                .AddFactory<IFileNameSearch>(this)
                .AddFactory<IFilePathSearch>(this)
                .AddFactory<IDirectoryNameSearch>(this)
                .AddFactory<IDirectoryPathSearch>(this);
        }

        IDirectoryCreator IActivator<IDirectoryCreator>.Create()
        {
            return new LocalDirectoryCreator(AbsolutePath);
        }

        IFileCreator IActivator<IFileCreator>.Create()
        {
            return new LocalFileCreator(AbsolutePath);
        }

        IDirectoryEnumerator IActivator<IDirectoryEnumerator>.Create()
        {
            return new LocalDirectoryEnumerator(AbsolutePath);
        }

        IFileEnumerator IActivator<IFileEnumerator>.Create()
        {
            return new LocalFileEnumerator(AbsolutePath);
        }

        IFileNameSearch IActivator<IFileNameSearch>.Create()
        {
            return new LocalSearchProvider(AbsolutePath);
        }

        IFilePathSearch IActivator<IFilePathSearch>.Create()
        {
            return new LocalSearchProvider(AbsolutePath);
        }

        IDirectoryNameSearch IActivator<IDirectoryNameSearch>.Create()
        {
            return new LocalSearchProvider(AbsolutePath);
        }

        IDirectoryPathSearch IActivator<IDirectoryPathSearch>.Create()
        {
            return new LocalSearchProvider(AbsolutePath);
        }

        /// <summary>
        /// Sets directory related properties from <paramref name="absolutePath"/>.
        /// </summary>
        /// <param name="absolutePath">Standard file system path to directory.</param>
        private void SetDirectoryRelatedProperties(string absolutePath)
        {
            if (!Directory.Exists(absolutePath))
                throw Ensure.Exception.Argument("absolutePath", "Provided path must be existing directory.");

            Name = Path.GetFileName(absolutePath);
        }

    
        /// <summary>
        /// Returns instance of <see cref="SearchOption"/> from <paramref name="inAllDescendants"/>.
        /// </summary>
        /// <param name="inAllDescendants">True for not only direct childs.</param>
        /// <returns>Instance of <see cref="SearchOption"/> from <paramref name="inAllDescendants"/>.</returns>
        private SearchOption GetSearchOption(bool inAllDescendants)
        {
            SearchOption searchOption = SearchOption.TopDirectoryOnly;
            if (inAllDescendants)
                searchOption = SearchOption.AllDirectories;

            return searchOption;
        }
    }
}
