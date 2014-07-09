using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Services.Behaviors
{
    /// <summary>
    /// Provides handler input.
    /// </summary>
    /// <typeparam name="T">Type of input.</typeparam>
    public interface IForInput<T>
    {
        /// <summary>
        /// Handler input.
        /// </summary>
        T Input { set; }
    }
}
