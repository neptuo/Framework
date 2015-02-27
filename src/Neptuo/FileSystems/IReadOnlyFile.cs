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
    /// Represents (not updateable) file in virtual file system.
    /// </summary>
    public interface IReadOnlyFile : IDomainModel<IKey>
    {
        /// <summary>
        /// File name without extension.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// File extension.
        /// </summary>
        string Extension { get; }

        /// <summary>
        /// Link to parent directory.
        /// </summary>
        IDirectory Parent { get; }

        /// <summary>
        /// Size of file in bytes.
        /// </summary>
        long FileSize { get; }

        /// <summary>
        /// Returns file content.
        /// </summary>
        /// <returns>File content.</returns>
        Task<string> GetContentAsync();

        /// <summary>
        /// Returns file content as byte array.
        /// </summary>
        /// <returns>File content as byte array.</returns>
        Task<byte[]> GetContentAsByteArrayAsync();

        /// <summary>
        /// Returns file content as stream.
        /// </summary>
        /// <returns>File content as stream.</returns>
        Task<Stream> GetContentAsStreamAsync();
    }
}
