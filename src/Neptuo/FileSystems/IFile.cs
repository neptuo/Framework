using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems
{
    /// <summary>
    /// Represents file in virtual file system.
    /// </summary>
    public interface IFile : IReadOnlyFile
    {
        /// <summary>
        /// Overrides file content to <paramref name="fileContent"/>.
        /// </summary>
        /// <param name="fileContent">New file content.</param>
        Task SetContentAsync(string fileContent);

        /// <summary>
        /// Overrides file content to <paramref name="fileContent"/>.
        /// </summary>
        /// <param name="fileContent">New file content.</param>
        Task SetContentFromByteArrayAsync(byte[] fileContent);

        /// <summary>
        /// Overrides file content to <paramref name="fileContent"/>.
        /// </summary>
        /// <param name="fileContent">New file content.</param>
        Task SetContentFromStreamAsync(Stream fileContent);
    }
}
