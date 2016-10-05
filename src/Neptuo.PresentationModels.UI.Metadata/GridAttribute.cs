using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.UI.Metadata
{
    /// <summary>
    /// Defines position of field in the grid system.
    /// </summary>
    public class GridAttribute : Attribute
    {
        /// <summary>
        /// Gets the index of the column.
        /// </summary>
        public int ColumnIndex { get; private set; }

        /// <summary>
        /// Gets the number of horizontal cells on the left to spread the current one into.
        /// </summary>
        public int? ColumnSpan { get; private set; }

        /// <summary>
        /// Gets the index of the row.
        /// </summary>
        public int RowIndex { get; private set; }

        /// <summary>
        /// Gets the number of vertical cells on the bottom to spread the current one into.
        /// </summary>
        public int? RowSpan { get; private set; }
    }
}
