using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Text.Positions
{
    /// <summary>
    /// Extends <see cref="IDocumentPoint"/> with property for end position.
    /// So this class models range sub part of source content.
    /// </summary>
    public interface IDocumentSpan : IDocumentPoint
    {
        /// <summary>
        /// Line index of range end.
        /// </summary>
        int EndLineIndex { get; }

        /// <summary>
        /// Index at line of range end.
        /// </summary>
        int EndColumnIndex { get; }
    }
}
