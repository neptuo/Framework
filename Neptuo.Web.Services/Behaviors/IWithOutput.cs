using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Services.Behaviors
{
    /// <summary>
    /// Provides handler output.
    /// </summary>
    /// <typeparam name="T">Type of output.</typeparam>
    public interface IWithOutput<T>
    {
        /// <summary>
        /// Handler output.
        /// </summary>
        T Output { get; }
    }
}
