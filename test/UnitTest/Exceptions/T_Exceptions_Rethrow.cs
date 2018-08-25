using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptuo.Exceptions.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Exceptions
{
    [TestClass]
    public class T_Exceptions_Rethrow
    {
        [TestMethod]
        public void Rethrow()
        {
            IExceptionHandler handler = new ExceptionHandlerBuilder()
                .Handler(new ReThrowExceptionHandler());

            string message1 = null;
            string message2 = null;

            try
            {
                try
                {
                    Level1();
                }
                catch (Exception e1)
                {
                    message1 = e1.ToString();
                    Assert.IsTrue(message1.Contains("Level3"));
                    handler.Handle(e1);
                }
            }
            catch (Exception e2)
            {
                message2 = e2.ToString();
                Assert.IsTrue(message1.Contains("Level3"));
                Assert.IsTrue(message2.StartsWith(message1));
            }
        }

        private void Level1()
        {
            Level2();
        }

        private void Level2()
        {
            Level3();
        }

        private void Level3()
        {
            throw new InvalidOperationException("Exception");
        }
    }
}
