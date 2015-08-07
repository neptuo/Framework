using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems.Features
{
    /// <summary>
    /// Extensions for writing file content to <see cref="IFileContentAppender"/>.
    /// </summary>
    public static class _FileContentAppenderExtensions
    {
        /// <summary>
        /// Appends <paramref name="fileContent"/> to the file content.
        /// </summary>
        /// <param name="fileContent">Content to append to the file.</param>
        public static void AppendContent(this IFileContentAppender file, string fileContent)
        {
            Task task = file.AppendContentAsync(fileContent);
            if(!task.IsCompleted && !task.IsCanceled)
                task.RunSynchronously();
        }

        /// <summary>
        /// Appends <paramref name="fileContent"/> to the file content.
        /// </summary>
        /// <param name="fileContent">Content to append to the file.</param>
        public static void AppendContentFromByteArray(this IFileContentAppender file, byte[] fileContent)
        {
            Task task = file.AppendContentFromByteArrayAsync(fileContent);
            if (!task.IsCompleted && !task.IsCanceled)
                task.RunSynchronously();
        }

        /// <summary>
        /// Appends <paramref name="fileContent"/> to the file content.
        /// </summary>
        /// <param name="fileContent">Content to append to the file.</param>
        public static void AppendContentFromStream(this IFileContentAppender file, Stream fileContent)
        {
            Task task = file.AppendContentFromStreamAsync(fileContent);
            if (!task.IsCompleted && !task.IsCanceled)
                task.RunSynchronously();
        }
    }
}
