using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Security.Cryptography
{
    /// <summary>
    /// Hash computer.
    /// </summary>
    /// <param name="source">Plain text value.</param>
    /// <returns>Hashed value from <paramref name="source"/>.</returns>
    public delegate string HashFunc(string source);
}
