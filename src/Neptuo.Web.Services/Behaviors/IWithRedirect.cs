using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Services.Behaviors
{
    /// <summary>
    /// If <see cref="IWithRedirect.Location"/> is not null, client is directed to provided url.
    /// </summary>
    public interface IWithRedirect : IWithStatus
    {
        /// <summary>
        /// Redirect url.
        /// </summary>
        string Location { get; }
    }
}
