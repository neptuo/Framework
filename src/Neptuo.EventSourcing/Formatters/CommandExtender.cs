using Neptuo.Collections.Specialized;
using Neptuo.Commands;
using Neptuo.Models.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Formatters
{
    /// <summary>
    /// Provides methods for setting <see cref="Command"/> key.
    /// </summary>
    public class CommandExtender
    {
        /// <summary>
        /// Sets <see cref="Command.Key"/> to a <paramref name="commandKey"/>.
        /// </summary>
        /// <param name="payload">An command to change.</param>
        /// <param name="commandKey">A key to set as the command key.</param>
        public void SetKey(Command payload, IKey commandKey) => payload.Key = commandKey;
    }
}
