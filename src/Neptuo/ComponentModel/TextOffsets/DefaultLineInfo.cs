using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.ComponentModel.TextOffsets
{
    /// <summary>
    /// Default implementation of <see cref="ILineInfo"/>.
    /// </summary>
    public class DefaultLineInfo : ILineInfo
    {
        public int LineIndex { get; private set; }
        public int ColumnIndex { get; private set; }

        public DefaultLineInfo(int lineIndex, int columnIndex)
        {
            LineIndex = lineIndex;
            ColumnIndex = columnIndex;
        }
    }
}
