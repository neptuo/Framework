using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Exceptions
{
    [TestClass]
    public class TestExceptionDispatchInfo
    {
        [TestMethod]
        public void CompareStackTraces()
        {
            Exception e1 = null;
            Exception e2 = null;
            Exception e3 = null;

            try
            {
                Method1();
            }
            catch (NotSupportedException e)
            {
                e1 = e;
            }

            try
            {
                Method2();
            }
            catch (NotSupportedException e)
            {
                e2 = e;
            }

            try
            {
                Method3();
            }
            catch (NotSupportedException e)
            {
                e3 = e;
            }

            Assert.IsNotNull(e1);
            Assert.IsNotNull(e2);
            Assert.IsNotNull(e3);

            Assert.IsTrue(e1.StackTrace.Contains("Method1"));
            Assert.IsTrue(e1.StackTrace.Contains("Throw"));

            Assert.IsTrue(e2.StackTrace.Contains("Method2"));
            Assert.IsFalse(e2.StackTrace.Contains("Throw"));

            Assert.IsFalse(e3.StackTrace.Contains("Method3"));
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
