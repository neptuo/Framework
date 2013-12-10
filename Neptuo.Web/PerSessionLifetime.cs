using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web
{
    public class PerSessionLifetime
    {
        /// <summary>
        /// Sets TTL for request.
        /// </summary>
        public int? HopCount { get; set; }

        public PerSessionLifetime(int? hopCount = null)
        {
            HopCount = hopCount;
        }
    }
}
