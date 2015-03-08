using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Pipelines.Validators.Messages
{
    /// <summary>
    /// Describes validation message.
    /// Not necessary must be error message.
    /// </summary>
    public interface IValidationMessage
    {
        /// <summary>
        /// Key (or property name) this message is bound to.
        /// </summary>
        string Key { get; }
        
        /// <summary>
        /// Text representation of message.
        /// </summary>
        string Message { get; }
    }
}
