using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo
{
    /// <summary>
    /// A collection of <see cref="ISchedulingContext"/>
    /// </summary>
    public interface ISchedulingCollection
    {
        /// <summary>
        /// Returns an enumeration of all scheduled contexts.
        /// </summary>
        /// <returns>An enumeration of all scheduled contexts.</returns>
        IEnumerable<ISchedulingContext> Enumerate();

        /// <summary>
        /// Adds <paramref name="context"/> to the collection.
        /// </summary>
        /// <param name="context">A context to be scheduled.</param>
        /// <returns>Self (for fluency).</returns>
        ISchedulingCollection Add(ISchedulingContext context);

        /// <summary>
        /// Removes <paramref name="context"/> from the collection.
        /// </summary>
        /// <param name="context">A context to be removed.</param>
        /// <returns>Self (for fluency).</returns>
        ISchedulingCollection Remove(ISchedulingContext context);

        /// <summary>
        /// Returns <c>true</c> if <paramref name="context"/> is contained in the collection.
        /// </summary>
        /// <param name="context"></param>
        /// <returns><c>true</c> if <paramref name="context"/> is contained in the collection; <c>false</c> otherwise.</returns>
        bool IsContained(ISchedulingContext context);
    }
}
