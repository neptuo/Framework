using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.ComponentModel.TextOffsets
{
    /// <summary>
    /// Describes text content info.
    /// </summary>
    public interface IContentRangeInfo
    {
        /// <summary>
        /// Starting index.
        /// </summary>
        int StartIndex { get; }

        /// <summary>
        /// Length of the content.
        /// </summary>
        int Length { get; }
    }
}
