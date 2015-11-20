using Neptuo.Models.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Models.Deleters
{
    /// <summary>
    /// Describes reference to other object.
    /// </summary>
    public interface IDeleteReference
    {
        /// <summary>
        /// Key of the referenced object.
        /// </summary>
        IKey Key { get; }

        /// <summary>
        /// Whether reference is mandatory (to target object must be deleted too).
        /// </summary>
        bool IsMandatory { get; }
    }
}
