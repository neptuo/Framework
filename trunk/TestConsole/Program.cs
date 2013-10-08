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

            Console.ReadKey(true);
        }
    }
}
