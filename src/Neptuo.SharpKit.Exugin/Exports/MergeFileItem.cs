using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpKit.UnobtrusiveFeatures.Exports
{
    /// <summary>
    /// Model for merging sources.
    /// </summary>
    public class MergeFileItem
    {
        /// <summary>
        /// Target file path.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// List of source file paths.
        /// </summary>
        public string[] Sources { get; set; }

        /// <summary>
        /// True to minify <see cref="MergeFileItem.FileName"/>.
        /// </summary>
        public bool Minify { get; set; }
    }
}
