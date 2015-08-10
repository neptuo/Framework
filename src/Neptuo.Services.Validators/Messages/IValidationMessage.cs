using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Services.Validators.Messages
{
    /// <summary>
    /// Describes validation message.
    /// Not necessary must be error message.
    /// </summary>
    public interface IValidationMessage
    {
        /// <summary>
        /// Target field key.
        /// </summary>
        string Key { get; }
        
        /// <summary>
        /// Returns text representation of message.
        /// </summary>
        string ToString();
    }
}
