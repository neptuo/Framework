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
    public interface ITextSpan
    {
        /// <summary>
        /// Starting index.
        /// </summary>
        int StartIndex { get; }

        /// <summary>
        /// Length of the span.
        /// </summary>
        int Length { get; }
    }
}
