using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Auditing
{
    /// <summary>
    /// An auditing decorator for envelopes just before they a processed.
    /// </summary>
    public interface IEnvelopeDecorator
    {
        /// <summary>
        /// Decorates <paramref name="envelope"/> with reqired metadata.
        /// </summary>
        /// <param name="envelope">An envelope to decorate.</param>
        void Decorate(Envelope envelope);
    }
}
