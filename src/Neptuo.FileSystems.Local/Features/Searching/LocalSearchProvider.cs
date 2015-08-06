using Neptuo.FileSystems.Internals;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems.Features.Searching
{
    /// <summary>
    /// Directory and file searching on local file system.
    /// </summary>
    public class LocalSearchProvider : IDirectoryNameSearch, IDirectoryPathSearch, IFileNameSearch, IFilePathSearch
    {
        private readonly string parentDirectory;

        /// <summary>
        /// Creates new instance that searches in <paramref name="parentDirectory"/>.
        /// </summary>
        /// <param name="parentDirectory">Root directory to search in.</param>
        public LocalSearchProvider(string parentDirectory)
        {
            Ensure.NotNullOrEmpty(parentDirectory, "parentDirectory");
            this.parentDirectory = parentDirectory;
        }

        private string PrepareSearchPattern(TextSearch search)
        {
            switch (search.Type)
            {
                case TextSearchType.Prefixed:
                    return "*" + search.Text;
                case TextSearchType.Suffixed:
                    return search.Text + "*";
                case TextSearchType.Contained:
                    return "*" + search.Text + "*";
                case TextSearchType.Matched:
                    return search.Text;
                default:
                    throw Ensure.Exception.NotSupported("TextSearchType '{0}' is not supported.", search.Type);
            }
        }

        private IEnumerable<string> EnumeratedDirectories(TextSearch nameOrPath, SearchOption searchOption)
        {
            return Directory
                .EnumerateDirectories(parentDirectory, PrepareSearchPattern(nameOrPath), searchOption);
        }

        private IEnumerable<string> EnumerateFiles(TextSearch nameOrPath, TextSearch fileExtension, SearchOption searchOption)
        {
            return Directory
                .EnumerateFiles(parentDirectory, PrepareSearchPattern(nameOrPath))
                .Where(file => IsExtensionMatched(file, fileExtension));
        }

        private bool IsExtensionMatched(string filePath, TextSearch search)
        {
            string fileExtension = Path.GetExtension(filePath);
            string searchExtension = search.Text;

            if (search.Text == null)
                return true;

            if (search.Text == String.Empty)
                return String.IsNullOrEmpty(fileExtension);

            if (!search.IsCaseSensitive)
            {
                fileExtension = fileExtension.ToLowerInvariant();
                searchExtension = searchExtension.ToLowerInvariant();
            }

            switch (search.Type)
            {
                case TextSearchType.Prefixed:
                    return fileExtension.StartsWith(searchExtension);
                case TextSearchType.Suffixed:
                    return fileExtension.EndsWith(searchExtension);
                case TextSearchType.Contained:
                    return fileExtension.Contains(searchExtension);
                case TextSearchType.Matched:
                    return fileExtension == searchExtension;
                default:
                    throw Ensure.Exception.NotSupported("TextSearchType '{0}' is not supported.", search.Type);
            }
        }

        #region IDirectoryNameSearch

        IEnumerable<IDirectory> IDirectoryNameSearch.FindDirectories(TextSearch directoryName)
        {
            return EnumeratedDirectories(directoryName, SearchOption.TopDirectoryOnly)
                .Select(directory => new LocalDirectory(directory));
        }

        bool IDirectoryNameSearch.IsDirectoryContained(TextSearch directoryName)
        {
            return EnumeratedDirectories(directoryName, SearchOption.TopDirectoryOnly)
                .Any();
        }

        #endregion

        #region IDirectoryPathSearch

        IEnumerable<IDirectory> IDirectoryPathSearch.FindDirectories(TextSearch directoryPath)
        {
            return EnumeratedDirectories(directoryPath, SearchOption.AllDirectories)
                .Select(directory => new LocalDirectory(directory));
        }

        bool IDirectoryPathSearch.IsDirectoryContained(TextSearch directoryPath)
        {
            return EnumeratedDirectories(directoryPath, SearchOption.AllDirectories)
                .Any();
        }

        #endregion

        #region IFileNameSearch

        IEnumerable<IFile> IFileNameSearch.FindFiles(TextSearch fileName, TextSearch fileExtension)
        {
            return EnumerateFiles(fileName, fileExtension, SearchOption.TopDirectoryOnly)
                .Select(file => new LocalFile(file));
        }

        bool IFileNameSearch.IsFileContained(TextSearch fileName, TextSearch fileExtension)
        {
            return EnumerateFiles(fileName, fileExtension, SearchOption.TopDirectoryOnly)
                .Any();
        }

        #endregion

        #region IFilePathSearch

        IEnumerable<IFile> IFilePathSearch.FindFiles(TextSearch filePath, TextSearch fileExtension)
        {
            return EnumerateFiles(filePath, fileExtension, SearchOption.AllDirectories)
                .Select(file => new LocalFile(file));
        }

        bool IFilePathSearch.IsFileContained(TextSearch filePath, TextSearch fileExtension)
        {
            return EnumerateFiles(filePath, fileExtension, SearchOption.AllDirectories)
                .Any();
        }

        #endregion
    }
}
