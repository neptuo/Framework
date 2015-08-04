using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Text.Positions
{
    /// <summary>
    /// Describes position/offset in source global source content.
    /// </summary>
    public interface IDocumentPoint
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
