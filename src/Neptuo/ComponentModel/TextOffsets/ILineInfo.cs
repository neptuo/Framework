using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.ComponentModel.TextOffsets
{
    /// <summary>
    /// Describes position/offset in source global source content.
    /// </summary>
    public interface ILineInfo
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
