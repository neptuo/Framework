using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems.Features
{
    /// <summary>
    /// Defines reference to file system owning current item.
    /// </summary>
    public interface IOwnerFileSystem
    {
        /// <summary>
        /// File system containing current item.
        /// </summary>
        IFileSystem FileSystem { get; }
    }
}
