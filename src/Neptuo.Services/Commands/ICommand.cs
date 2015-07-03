using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Services.Commands
{
    /// <summary>
    /// Optional base.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Unique command identifier.
        /// </summary>
        string Guid { get; }
    }
}
