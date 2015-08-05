using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems.Features
{
    /// <summary>
    /// Extensions for writing file content to <see cref="IFileContentUpdater"/>.
    /// </summary>
    public static class _FileContentUpdaterExtensions
    {
        /// <summary>
        /// Overrides file content to <paramref name="fileContent"/>.
        /// </summary>
        /// <param name="fileContent">New file content.</param>
        public static void SetContent(this IFileContentUpdater file, string fileContent)
        {
            Task task = file.SetContentAsync(fileContent);
            if(!task.IsCompleted && !task.IsCanceled)
                task.RunSynchronously();
        }

        /// <summary>
        /// Overrides file content to <paramref name="fileContent"/>.
        /// </summary>
        /// <param name="fileContent">New file content.</param>
        public static void SetContentFromByteArray(this IFileContentUpdater file, byte[] fileContent)
        {
            Task task = file.SetContentFromByteArrayAsync(fileContent);
            if (!task.IsCompleted && !task.IsCanceled)
                task.RunSynchronously();
        }

        /// <summary>
        /// Overrides file content to <paramref name="fileContent"/>.
        /// </summary>
        /// <param name="fileContent">New file content.</param>
        public static void SetContentFromStream(this IFileContentUpdater file, Stream fileContent)
        {
            Task task = file.SetContentFromStreamAsync(fileContent);
            if (!task.IsCompleted && !task.IsCanceled)
                task.RunSynchronously();
        }
    }
}
