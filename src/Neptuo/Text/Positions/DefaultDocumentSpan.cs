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
    public class DefaultDocumentSpan : IDocumentSpan
    {
        public int ColumnIndex { get; private set; }
        public int LineIndex { get; private set; }

        public int EndColumnIndex { get; private set; }
        public int EndLineIndex { get; private set; }

        public DefaultDocumentSpan(int columnIndex, int lineIndex, int endColumnIndex, int endLineIndex)
        {
            ColumnIndex = columnIndex;
            LineIndex = lineIndex;

            EndColumnIndex = endColumnIndex;
            EndLineIndex = endLineIndex;
        }

        public override string ToString()
        {
            return String.Format("<{0}:{1}, {2}:{3}>", LineIndex, ColumnIndex, EndLineIndex, EndColumnIndex);
        }
    }
}
