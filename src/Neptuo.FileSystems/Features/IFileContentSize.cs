using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems.Features
{
    /// <summary>
    /// Contract for reading file size.
    /// </summary>
    public interface IFileContentSize
    {
        /// <summary>
        /// Size of file in bytes.
        /// </summary>
        long FileSize { get; }
    }
}
