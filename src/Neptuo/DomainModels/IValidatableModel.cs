using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.DomainModels
{
    /// <summary>
    /// Decribes model that contains information if the model is in valid state.
    /// This information is mostly set from <see cref="Pipelines.Validators.IValidationDispatcher"/>.
    /// </summary>
    public interface IValidatableModel
    {
        /// <summary>
        /// Whether the model is in valid state; <c>null</c> = not validated, <c>true</c> = is valid, <c>false</c> = is not valid.
        /// </summary>
        bool? IsValid { get; set; }
    }
}
