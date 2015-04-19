using Neptuo.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.ComponentModel.TextOffsets
{
    /// <summary>
    /// Default implementation of <see cref="ILineRangeInfo"/>.
    /// </summary>
    public class DefaultLineRangeInfo : ILineRangeInfo
    {
        public int ColumnIndex { get; private set; }
        public int LineIndex { get; private set; }

        public int EndColumnIndex { get; private set; }
        public int EndLineIndex { get; private set; }

        public DefaultLineRangeInfo(int columnIndex, int lineIndex, int endColumnIndex, int endLineIndex)
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
