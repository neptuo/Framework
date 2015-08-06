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
            Ensure.NotNullOrEmpty(parentDirectory, "parentDirectory");
            this.parentDirectory = parentDirectory;
        }

        public IDirectory Create(string directoryName)
        {
            Ensure.NotNullOrEmpty(directoryName, "directoryName");

            foreach (char invalidChar in Path.GetInvalidPathChars())
            {
                if (directoryName.Contains(invalidChar))
                    throw Ensure.Exception.ArgumentOutOfRange("directoryName", "Directory name '{0}' contains invalid char '{1}'.", directoryName, invalidChar);
            }

            string newPath = Path.Combine(parentDirectory, directoryName);
            Directory.CreateDirectory(newPath);
            return new LocalDirectory(newPath);
        }

        public bool IsValidName(string directoryName)
        {
            foreach (char invalidChar in Path.GetInvalidPathChars())
            {
                if (directoryName.Contains(invalidChar))
                    return false;
            }

            return true;
        }
    }
}
