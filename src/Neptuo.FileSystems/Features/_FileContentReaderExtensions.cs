using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems.Features
{
    /// <summary>
    /// Extensions for reading file content from <see cref="IFileContentReader"/>.
    /// </summary>
    public static class _FileContentReaderExtensions
    {
        /// <summary>
        /// Returns file content.
        /// </summary>
        /// <returns>File content.</returns>
        public static string GetContent(this IFileContentReader file)
        {
            Ensure.NotNull(file, "file");

            Task<string> task = file.GetContentAsync();
            if(!task.IsCompleted && !task.IsCanceled)
                task.RunSynchronously();

            return task.Result;
        }

        /// <summary>
        /// Returns file content as byte array.
        /// </summary>
        /// <returns>File content as byte array.</returns>
        public static byte[] GetContentAsByteArray(this IFileContentReader file)
        {
            Ensure.NotNull(file, "file");

            Task<byte[]> task = file.GetContentAsByteArrayAsync();
            if(!task.IsCompleted && !task.IsCanceled)
                task.RunSynchronously();

            return task.Result;
        }

        /// <summary>
        /// Returns file content as stream.
        /// </summary>
        /// <returns>File content as stream.</returns>
        public static Stream GetContentAsStream(this IFileContentReader file)
        {
            Ensure.NotNull(file, "file");

            Task<Stream> task = file.GetContentAsStreamAsync();
            if(!task.IsCompleted && !task.IsCanceled)
                task.RunSynchronously();

            return task.Result;
        }
    }
}
