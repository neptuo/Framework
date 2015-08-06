using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems.Features
{
    /// <summary>
    /// File creator on local file system.
    /// </summary>
    public class LocalFileCreator : IFileCreator
    {
        private readonly string parentDirectory;

        public LocalFileCreator(string parentDirectory)
        {
            Ensure.NotNullOrEmpty(parentDirectory, "parentDirectory");
            this.parentDirectory = parentDirectory;
        }

        public IFile Create(string fileName, string fileExtension)
        {
            foreach (char invalidChar in Path.GetInvalidFileNameChars())
            {
                if (fileName != null && fileName.Contains(invalidChar))
                        throw Ensure.Exception.ArgumentOutOfRange("fileName", "File name '{0}' contains invalid char '{1}'.", fileName, invalidChar);

                if (fileExtension != null && fileExtension.Contains(invalidChar))
                    throw Ensure.Exception.ArgumentOutOfRange("fileExtension", "File extension '{0}' contains invalid char '{1}'.", fileExtension, invalidChar);
            }

            string newPath = Path.Combine(parentDirectory, String.Join("{0}.{1}", fileName, fileExtension));
            File.Create(newPath).Dispose();
            return new LocalFile(newPath);
        }

        public bool IsValidName(string fileName, string fileExtension)
        {
            foreach (char invalidChar in Path.GetInvalidFileNameChars())
            {
                if (fileName != null && fileName.Contains(invalidChar))
                    return false;

                if (fileExtension != null && fileExtension.Contains(invalidChar))
                    return false;
            }

            return true;
        }
    }
}
