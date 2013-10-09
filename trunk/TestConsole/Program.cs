using System;
using System.Collections.Generic;
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
            //TestEntity.Test();
            TestPresentationModel.Test();

            //List<int> list = new List<int>();
            //list.Insert(0, 1);
            //list.Insert(1, 2);

            //Console.WriteLine(list[2]);
            //list.Insert(2, 3);

            Console.ReadKey(true);
        }
    }
}
