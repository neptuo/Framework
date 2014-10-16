using Neptuo.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole.Hashing
{
    class TestHash
    {
        public static void Test()
        {
            Console.WriteLine(HashService.Sha1("Hello, World!"));
            Console.WriteLine(HashService.Sha256("Hello, World!"));
        }
    }
}
