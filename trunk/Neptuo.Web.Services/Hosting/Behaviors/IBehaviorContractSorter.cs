using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Services.Hosting.Behaviors
{
    public interface IBehaviorContractSorter
    {
        /// <summary>
        /// Sorts behavior constracts according to execution order.
        /// </summary>
        /// <param name="behaviorContracts">Behavior constracts to sort.</param>
        /// <returns>Sorted behavior constracts.</returns>
        IEnumerable<Type> SortBehaviors(IEnumerable<Type> behaviorContracts);
    }
}
