using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.ComponentModel
{
    /// <summary>
    /// Generates identifiers using specified prefix and integer sequence.
    /// </summary>
    public class SequenceUniqueNameProvider : IUniqueNameProvider
    {
        /// <summary>
        /// Optional string prefix.
        /// </summary>
        private string prefix;

        /// <summary>
        /// Optional zero offset.
        /// </summary>
        private int offset;

        /// <summary>
        /// Creates instance with optional string prefix <paramref name="prefix"/> and optional int offset <paramref name="offset"/>.
        /// </summary>
        /// <param name="prefix">Optional string prefix.</param>
        /// <param name="offset">Optional zero offset.</param>
        public SequenceUniqueNameProvider(string prefix = null, int offset = 0)
        {
            Guard.PositiveOrZero(offset, "offset");
            this.prefix = prefix;
            this.offset = offset;
        }

        public string Next()
        {
            return prefix + (++offset);
        }
    }
}
