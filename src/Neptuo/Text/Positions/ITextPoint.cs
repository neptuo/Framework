using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Text.Positions
{
    /// <summary>
    /// Describes text content info.
    /// </summary>
    public interface ITextPoint
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
