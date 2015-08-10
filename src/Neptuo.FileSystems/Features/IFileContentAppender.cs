using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems.Features
{
    /// <summary>
    /// Provides contract for appending to file content.
    /// </summary>
    public interface IFileContentAppender
    {
        /// <summary>
        /// Appends <paramref name="fileContent"/> to the file content.
        /// </summary>
        /// <param name="fileContent">Content to append to the file.</param>
        Task AppendContentAsync(string fileContent);

        /// <summary>
        /// Appends <paramref name="fileContent"/> to the file content.
        /// </summary>
        /// <param name="fileContent">Content to append to the file.</param>
        Task AppendContentFromByteArrayAsync(byte[] fileContent);

        /// <summary>
        /// Appends <paramref name="fileContent"/> to the file content.
        /// </summary>
        /// <param name="fileContent">Content to append to the file.</param>
        Task AppendContentFromStreamAsync(Stream fileContent);
    }
}
