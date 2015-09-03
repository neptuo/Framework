using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Text.Positions
{
    /// <summary>
    /// Default implementation of <see cref="IDocumentSpan"/>.
    /// </summary>
    public class DefaultDocumentSpan : DefaultDocumentPoint, IDocumentSpan
    {
        public int EndLineIndex { get; private set; }
        public int EndColumnIndex { get; private set; }

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="lineIndex">Line index.</param>
        /// <param name="columnIndex">Index at line.</param>
        /// <param name="endLineIndex">Line index of range end.</param>
        /// <param name="endColumnIndex">Index at line of range end.</param>
        public DefaultDocumentSpan(int lineIndex, int columnIndex, int endLineIndex, int endColumnIndex)
            : base(lineIndex, columnIndex)
        {
            EndColumnIndex = endColumnIndex;
            EndLineIndex = endLineIndex;
        }

        public override string ToString()
        {
            return String.Format("<{0}:{1}, {2}:{3}>", LineIndex, ColumnIndex, EndLineIndex, EndColumnIndex);
        }
    }
}
