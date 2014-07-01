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
    public interface IReadOnlyFile
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
        /// Path from root containing this file name and extension.
        /// </summary>
        string FullPath { get; }

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
        string GetContent();

        /// <summary>
        /// Returns file content as byte array.
        /// </summary>
        /// <returns>File content as byte array.</returns>
        byte[] GetContentAsByteArray();

        /// <summary>
        /// Returns file content as stream.
        /// </summary>
        /// <returns>File content as stream.</returns>
        Stream GetContentAsStream();
    }
}
