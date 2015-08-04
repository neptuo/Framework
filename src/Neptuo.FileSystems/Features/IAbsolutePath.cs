using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems.Features
{
    /// <summary>
    /// Provides absolute path (excluding this item).
    /// </summary>
    public interface IAbsolutePath
    {
        /// <summary>
        /// Absolute path (excluding this item).
        /// </summary>
        string Path { get; }
    }
}
