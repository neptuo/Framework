using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Security.Cryptography
{
    /// <summary>
    /// Service for various hashing.
    /// </summary>
    public static class HashService
    {
        private static HashProvider sha1;
        private static readonly object sha1Lock = new object();

        private static HashProvider sha256;
        private static readonly object sha256Lock = new object();

        /// <summary>
        /// Provides SHA1 hash provider.
        /// </summary>
        public static HashProvider Sha1
        {
            get
            {
                if (sha1 == null)
                {
                    lock (sha1Lock)
                    {
                        if (sha1 == null)
                            sha1 = CreateSha1();
                    }
                }

                return sha1;
            }
        }

        /// <summary>
        /// Provides SHA256 hash provider.
        /// </summary>
        public static HashProvider Sha256
        {
            get
            {
                if (sha256 == null)
                {
                    lock (sha256Lock)
                    {
                        if (sha256 == null)
                            sha256 = CreateSha256();
                    }
                }

                return sha256;
            }
        }

        /// <summary>
        /// Creates delegate for computing hashes using <paramref name="algorithm"/>.
        /// </summary>
        /// <param name="algorithm">Algorithm for compution hashes.</param>
        /// <returns></returns>
        private static HashProvider CreateProvider(HashAlgorithm algorithm)
        {
            Guard.NotNull(algorithm, "algorithm");
            return (source) =>
            {
                byte[] hash = algorithm.ComputeHash(Encoding.UTF8.GetBytes(source));
                StringBuilder result = new StringBuilder();
                foreach (byte hashPart in hash)
                    result.Append(hashPart.ToString("X2"));

                return result.ToString();
            };
        }

        /// <summary>
        /// Creates delegate for computing SHA1 hashes.
        /// </summary>
        private static HashProvider CreateSha1()
        {
            return CreateProvider(SHA1.Create());
        }

        /// <summary>
        /// Creates delegate for computing SHA256 hashes.
        /// </summary>
        private static HashProvider CreateSha256()
        {
            return CreateProvider(SHA256.Create());
        }
    }
}
