using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo
{
    /// <summary>
    /// Provider for unique identifiers.
    /// </summary>
    public interface IGuidProvider
    {
        /// <summary>
        /// Generates new unique identifier.
        /// </summary>
        /// <returns>New unique identifier.</returns>
        string Next();
    }
}
