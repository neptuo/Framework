using Neptuo.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems.Events
{
    /// <summary>
    /// Provides file change events (<see cref="FileCreated"/>, <see cref="FileRenamed"/>, <see cref="FileDeleted"/>).
    /// Uses underlaying <see cref="FileSystemWatcher"/> for monitoring file system changes.
    /// Must be disposed to dispose instance of <see cref="FileSystemWatcher"/>.
    /// </summary>
    public class LocalFileSystemWatcher : DisposableBase
    {
        private readonly FileSystemWatcher watcher;
        private readonly IEventDispatcher eventDispatcher;

        /// <summary>
        /// Creates new instance 
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <param name="eventDispatcher"></param>
        public LocalFileSystemWatcher(string directoryPath, IEventDispatcher eventDispatcher)
        {
            Ensure.Condition.DirectoryExists(directoryPath, "directoryPath");
            Ensure.NotNull(eventDispatcher, "eventDispatcher");
            this.watcher = new FileSystemWatcher(directoryPath);
            this.eventDispatcher = eventDispatcher;
            
            watcher.BeginInit();
            watcher.EnableRaisingEvents = true;
            watcher.IncludeSubdirectories = true;
            watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName;
            watcher.Created += OnFileCreated;
            watcher.Deleted += OnFileDeleted;
            watcher.Renamed += OnFileRenamed;
            watcher.EndInit();
        }

        private void OnFileCreated(object sender, FileSystemEventArgs e)
        {
            eventDispatcher.PublishAsync(new FileCreated(LocalFileSystem.FromFilePath(e.FullPath)));
        }

        private void OnFileDeleted(object sender, FileSystemEventArgs e)
        {
            eventDispatcher.PublishAsync(new FileDeleted(LocalFileSystem.FromFilePath(e.FullPath)));
        }

        private void OnFileRenamed(object sender, RenamedEventArgs e)
        {
            eventDispatcher.PublishAsync(new FileRenamed(LocalFileSystem.FromFilePath(e.FullPath)));
        }

        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();
            watcher.Dispose();
        }
    }
}
