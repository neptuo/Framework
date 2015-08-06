using Neptuo.Activators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems.Features
{
    /// <summary>
    /// New file creator.
    /// </summary>
    public interface IFileCreator
    {
        /// <summary>
        /// Creates new empty file.
        /// </summary>
        /// <param name="fileName">File name (without extension).</param>
        /// <param name="fileExtension">File extension (without extension separator).</param>
        /// <returns>Newly created file.</returns>
        IFile Create(string fileName, string fileExtension);

        /// <summary>
        /// Returns <c>true</c> if <paramref name="fileName"/> with <paramref name="fileExtension" /> is valid file name.
        /// </summary>
        /// <param name="fileName">File name (without extension).</param>
        /// <param name="fileExtension">File extension (without extension separator).</param>
        /// <returns><c>true</c> if <paramref name="fileName"/> with <paramref name="fileExtension" /> is valid file name; <c>false</c> otherwise.</returns>
        bool IsValidName(string fileName, string fileExtension);
    }
}
