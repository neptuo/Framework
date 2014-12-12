using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems
{
    /// <summary>
    /// Extensions for reading and writing file content in sync mode.
    /// </summary>
    public static class _FileContentExtensions
    {
        /// <summary>
        /// Returns file content.
        /// </summary>
        /// <returns>File content.</returns>
        public static string GetContent(this IReadOnlyFile file)
        {
            Guard.NotNull(file, "file");
            return file.GetContentAsync().Result;
        }

        /// <summary>
        /// Returns file content as byte array.
        /// </summary>
        /// <returns>File content as byte array.</returns>
        public static byte[] GetContentAsByteArray(this IReadOnlyFile file)
        {
            Guard.NotNull(file, "file");
            return file.GetContentAsByteArrayAsync().Result;
        }

        /// <summary>
        /// Returns file content as stream.
        /// </summary>
        /// <returns>File content as stream.</returns>
        public static Stream GetContentAsStream(this IReadOnlyFile file)
        {
            Guard.NotNull(file, "file");
            return file.GetContentAsStreamAsync().Result;
        }

        /// <summary>
        /// Overrides file content to <paramref name="fileContent"/>.
        /// </summary>
        /// <param name="fileContent">New file content.</param>
        public static void SetContent(this IFile file, string fileContent)
        {
            file.SetContentAsync(fileContent).Wait();
        }

        /// <summary>
        /// Overrides file content to <paramref name="fileContent"/>.
        /// </summary>
        /// <param name="fileContent">New file content.</param>
        public static void SetContentFromByteArray(this IFile file, byte[] fileContent)
        {
            file.SetContentFromByteArrayAsync(fileContent).Wait();
        }

        /// <summary>
        /// Overrides file content to <paramref name="fileContent"/>.
        /// </summary>
        /// <param name="fileContent">New file content.</param>
        public static void SetContentFromStream(this IFile file, Stream fileContent)
        {
            file.SetContentFromStreamAsync(fileContent).Wait();
        }
    }
}
