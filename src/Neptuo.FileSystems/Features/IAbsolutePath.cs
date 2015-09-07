using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems.Features
{
    /// <summary>
    /// Provides absolute path (including this item).
    /// </summary>
    public interface IAbsolutePath
    {
        /// <summary>
        /// Absolute path (including this item).
        /// </summary>
        string AbsolutePath { get; }
    }
}
