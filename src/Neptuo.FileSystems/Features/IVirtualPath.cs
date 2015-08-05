using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems.Features
{
    /// <summary>
    /// Provides virtual path (excluding this item).
    /// </summary>
    public interface IVirtualPath
    {
        /// <summary>
        /// Virtual path (excluding this item).
        /// </summary>
        string Virtual { get; }
    }
}
