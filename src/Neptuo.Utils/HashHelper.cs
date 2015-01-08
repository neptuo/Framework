using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Neptuo.Security.Cryptography
{
    public static class HashHelper
    {
        public static string Sha1(string text)
        {
            SHA1 hasher = SHA1.Create();
            byte[] hash = hasher.ComputeHash(Encoding.UTF8.GetBytes(text));
            StringBuilder result = new StringBuilder();
            foreach (byte hashPart in hash)
                result.Append(hashPart.ToString("X2"));

            return result.ToString();
        }
    }
}
