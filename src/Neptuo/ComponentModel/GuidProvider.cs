using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.ComponentModel
{
    /// <summary>
    /// Generates identifiers from <see cref="Guid"/>.
    /// </summary>
    public class GuidProvider : IUniqueNameProvider
    {
        public string Next()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
