using Neptuo.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestConsole.Hashing
{
    class TestHash : TestClass
    {
        public static void Test()
        {
            Console.WriteLine(HashProvider.Sha1("Hello, World!"));
            Console.WriteLine(HashProvider.Sha256("Hello, World!"));

            DebugIteration("Threads", 100, () =>
            {
                Thread t = new Thread(() => Console.WriteLine(HashProvider.Sha1("Hello, World!")));
                t.Start();
            });

            HashFunc hashFunc = new HashFactory().Sha1;
            DebugIteration("New", 1000, () => hashFunc("Hello, World!"));


        }
    }
}
