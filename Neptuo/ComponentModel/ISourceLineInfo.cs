using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.ComponentModel
{
    /// <summary>
    /// Describes position/offset in source global source content.
    /// </summary>
    public interface ISourceLineInfo
    {
        /// <summary>
        /// Line index.
        /// </summary>
        int LineIndex { get; }

        /// <summary>
        /// Index at line.
        /// </summary>
        int ColumnIndex { get; }
    }
}
