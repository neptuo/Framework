using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole.DispatcherExceptions
{
    class TestDispatcherException
    {
        public static void Test()
        {
            try
            {
                Method1();
            }
            catch (NotSupportedException e)
            {
                Console.WriteLine(e.StackTrace);
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            try
            {
                Method2();
            }
            catch (NotSupportedException e)
            {
                Console.WriteLine(e.StackTrace);
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            try
            {
                Method3();
            }
            catch (NotSupportedException e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private static void Method1()
        {
            try
            {
                Throw();
            }
            catch (NotSupportedException e)
            {
                ExceptionDispatchInfo info = ExceptionDispatchInfo.Capture(e);
                //Console.WriteLine(info);
                info.Throw();
            }
        }

        private static void Method2()
        {
            try
            {
                Throw();
            }
            catch (NotSupportedException e)
            {
                throw e;
            }
        }

        private static void Method3()
        {
            Throw();
        }

        private static void Throw()
        {
            throw new NotSupportedException();
        }
    }
}
