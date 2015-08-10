using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems.Features
{
    /// <summary>
    /// Directory creator on local file system.
    /// </summary>
    public class LocalDirectoryCreator : IDirectoryCreator
    {
        private readonly string parentDirectory;

        public LocalDirectoryCreator(string parentDirectory)
        {
            Ensure.Condition.DirectoryExists(parentDirectory, "parentDirectory");
            this.parentDirectory = parentDirectory;
        }

        public IDirectory Create(string directoryName)
        {
            Ensure.NotNullOrEmpty(directoryName, "directoryName");
            EnsureValidName(directoryName);

            string newPath = Path.Combine(parentDirectory, directoryName);
            Directory.CreateDirectory(newPath);
            return new LocalDirectory(newPath);
        }

        public bool IsValidName(string directoryName)
        {
            foreach (char invalidChar in Path.GetInvalidFileNameChars())
            {
                if (directoryName.Contains(invalidChar))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Throws <see cref="ArgumentOutOfRangeException"/> if <paramref name="directoryName"/> is not valid directory name.
        /// </summary>
        /// <param name="directoryName">Directory name to test.</param>
        public static void EnsureValidName(string directoryName)
        {
            foreach (char invalidChar in Path.GetInvalidFileNameChars())
            {
                if (directoryName.Contains(invalidChar))
                    throw Ensure.Exception.ArgumentOutOfRange("directoryName", "Directory name '{0}' contains invalid char '{1}'.", directoryName, invalidChar);
            }
        }
    }
}
