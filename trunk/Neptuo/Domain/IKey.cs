using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Domain
{
    /// <summary>
    /// Describes key of the domain model.
    /// </summary>
    public interface IKey : IEquatable<IKey>, IComparable
    {
        /// <summary>
        /// Identifier of the domain model type.
        /// </summary>
        string Type { get; }

        /// <summary>
        /// Whether this key is empty.
        /// </summary>
        bool IsEmpty { get; }
    }
}
