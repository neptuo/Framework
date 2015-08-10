using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems.Features
{
    /// <summary>
    /// Provides contract for overriding file content.
    /// </summary>
    public interface IFileContentUpdater
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
