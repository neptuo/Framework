using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestConsole.Data;
using TestConsole.PresentationModels;
using TestConsole.Tokens;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestTokens.Test();
            TestEntity.Test();
            //TestPresentationModel.Test();

            //List<int> list = new List<int>();
            //list.Insert(0, 1);
            //list.Insert(1, 2);

            //Console.WriteLine(list[2]);
            //list.Insert(2, 3);

            Console.ReadKey(true);
        }
    }

    class TestClass
    {
        public static void Debug(string title, Action action)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            action();

            sw.Stop();
            Console.WriteLine("{0}: {1}ms", title, sw.ElapsedMilliseconds);
        }

        public static T Debug<T>(string title, Func<T> action)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            T result = action();

            sw.Stop();
            Console.WriteLine("{0}: {1}ms", title, sw.ElapsedMilliseconds);
            return result;
        }
    }
}
