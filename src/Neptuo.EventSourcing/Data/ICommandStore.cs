using Neptuo.Models.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Data
{
    /// <summary>
    /// Describes the undelaying store for commands.
    /// </summary>
    public interface ICommandStore
    {
        /// <summary>
        /// Saves <paramref name="commands"/>.
        /// </summary>
        /// <param name="commands">The commands to save.</param>
        void Save(IEnumerable<CommandModel> commands);
    }
}
