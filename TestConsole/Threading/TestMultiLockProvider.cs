using Neptuo.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestConsole.Threading
{
    class TestMultiLockProvider : TestClass
    {
        public static void Test()
        {
            MultiLockProvider provider = new MultiLockProvider();
            //LockProvider provider = new LockProvider(new ReaderWriterLockSlim());
            //ReaderWriterLockSlim slimLock = new ReaderWriterLockSlim();

            for (int i = 0; i < 10; i++)
            {
                new Thread((index) =>
                {
                    using (provider.Lock("A"))
                    {
                        Console.Write("a{0}", index);
                        Thread.Sleep(1000);
                        Console.WriteLine(" -> a{0}", index);
                    }
                }).Start(i);
            }

            for (int i = 0; i < 10; i++)
            {
                new Thread((index) =>
                {
                    using (provider.Lock("B"))
                    {
                        Console.Write("b{0}", index);
                        Thread.Sleep(1000);
                        Console.WriteLine(" -> b{0}", index);
                    }
                }).Start(i);
            }
        }
    }
}
