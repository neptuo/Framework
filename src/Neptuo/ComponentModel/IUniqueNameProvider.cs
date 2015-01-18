using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.ComponentModel
{
    /// <summary>
    /// Provider for unique names.
    /// </summary>
    public interface IUniqueNameProvider
    {
        /// <summary>
        /// Generates new unique name.
        /// </summary>
        /// <returns>New unique name.</returns>
        string Next();
    }
}
