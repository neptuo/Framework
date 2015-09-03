using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Text.Positions
{
    /// <summary>
    /// Default implementation of <see cref="ITextSpan"/>.
    /// </summary>
    public class DefaultTextSpan : ITextSpan
    {
        public int StartIndex { get; private set; }
        public int Length { get; private set; }

        /// <summary>
        /// Create new instance.
        /// </summary>
        /// <param name="startIndex">Starting index.</param>
        /// <param name="length">Length of the span.</param>
        public DefaultTextSpan(int startIndex, int length)
        {
            StartIndex = startIndex;
            Length = length;
        }

        public override string ToString()
        {
            return String.Format("<{0}, {1}>", StartIndex, StartIndex + Length);
        }
    }
}
