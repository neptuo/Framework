using Neptuo.Collections.Specialized;
using Neptuo.Text.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole.Tokens
{
    static class TestTokenWriter
    {
        public static void Test()
        {
            TokenWriter writer = new TokenWriter("Hello, my name is {Name} and I am from {City}.");
            string result = writer.Format(new KeyValueCollection().Add("Name", "Peter").Add("City", "Prague"));
            Console.WriteLine(result);
        }
    }
}
