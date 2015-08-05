using Neptuo.Models.Features;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems
{
    /// <summary>
    /// Represents file in virtual file system.
    /// </summary>
    public interface IFile : IFeatureModel
    {
        /// <summary>
        /// File name without extension.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// File extension (without extensions separator).
        /// </summary>
        string Extension { get; }
    }
}
