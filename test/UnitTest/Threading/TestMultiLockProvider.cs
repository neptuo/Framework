using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Neptuo.Threading
{
    [TestClass]
    public class TestMultiLockProvider
    {
        [TestMethod]
        public void Basic()
        {
            MultiLockProvider provider = new MultiLockProvider();
            bool a = false;
            bool b = false;

            for (int i = 0; i < 10; i++)
            {
                new Thread((index) =>
                {
                    using (provider.Lock("A"))
                    {
                        Thread.Sleep(100);
                        Assert.IsFalse(a);
                        a = true;

                        Thread.Sleep(1000);
                        Assert.IsTrue(a);
                        a = false;
                    }
                }).Start(i);
            }

            for (int i = 0; i < 10; i++)
            {
                new Thread((index) =>
                {
                    using (provider.Lock("B"))
                    {
                        Thread.Sleep(100);
                        Assert.IsFalse(b);
                        b = true;

                        Thread.Sleep(1000);
                        Assert.IsTrue(b);
                        b = false;
                    }
                }).Start(i);
            }
        }
    }
}
