using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems.Features
{
    /// <summary>
    /// Provides contract for reading file content.
    /// </summary>
    public interface IFileContentReader
    {
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
