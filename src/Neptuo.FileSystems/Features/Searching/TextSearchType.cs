using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems.Features.Searching
{
    /// <summary>
    /// Enumeration of support text search types.
    /// </summary>
    public enum TextSearchType
    {
        /// <summary>
        /// Searched value must be prefixed with used value.
        /// </summary>
        Prefixed,

        /// <summary>
        /// Searched value must suffixed with used value.
        /// </summary>
        Suffixed,

        /// <summary>
        /// Search value must contain used value.
        /// </summary>
        Contained,

        /// <summary>
        /// Searched value must exactly match used value.
        /// </summary>
        Matched
    }
}
