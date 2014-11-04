using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Security.Cryptography
{
    /// <summary>
    /// Provider for various hashing functions (using static access).
    /// Targeted for single hash computing. When computing more hashes, use <see cref="HashFactory"/>.
    /// </summary>
    public class HashProvider
    {
        /// <summary>
        /// Provides SHA1 hash provider.
        /// </summary>
        public static HashFunc Sha1
        {
            get { return new HashFactory().Sha1; }
        }

        /// <summary>
        /// Provides SHA1 hash provider.
        /// </summary>
        public static HashFunc Sha256
        {
            get { return new HashFactory().Sha256; }
        }
    }
}
