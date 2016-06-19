using Neptuo.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole.Asyncing
{
    static class TestAsync
    {
        public static void Test()
        {
            Schedule(0).Wait();
            Schedule(1).Wait();
        }

        static Task Schedule(int index)
        {
            return Task.Factory.StartNew(async () =>
            {
                for (int i = 0; i < 5; i++)
                    await Execute(index, i);
            });
        }

        static async Task Execute(int index, int i)
        {
            Console.WriteLine($"Entering {index} at iteration {i}");
            await Task.Delay(100);
            Console.WriteLine($"Leaving  {index} at iteration {i}");
        }
    }
}
