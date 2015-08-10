using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Identifiers
{
    /// <summary>
    /// Generates identifiers from <see cref="Guid"/>.
    /// </summary>
    public class GuidUniqueNameProvider : IUniqueNameProvider
    {
        public string Next()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
