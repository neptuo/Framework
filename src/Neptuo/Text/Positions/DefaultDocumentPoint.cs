using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Text.Positions
{
    /// <summary>
    /// Default implementation of <see cref="IDocumentPoint"/>.
    /// </summary>
    public class DefaultDocumentPoint : IDocumentPoint
    {
        public int LineIndex { get; private set; }
        public int ColumnIndex { get; private set; }

        public DefaultDocumentPoint(int lineIndex, int columnIndex)
        {
            LineIndex = lineIndex;
            ColumnIndex = columnIndex;
        }
    }
}
