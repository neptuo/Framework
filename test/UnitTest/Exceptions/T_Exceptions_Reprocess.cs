using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptuo.Exceptions.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Neptuo.Exceptions
{
    [TestClass]
    public class T_Exceptions_Reprocess
    {
        private readonly AnyHandledHandler handler;
        private readonly Reprocess Reprocess;

        public T_Exceptions_Reprocess()
        {
            handler = new AnyHandledHandler();
            Reprocess = new Reprocess(handler, 3);
        }

        [TestMethod]
        public void RunAction()
        {
            handler.Count = 0;

            bool state = Reprocess.Run(() => throw new InvalidOperationException());
            Assert.AreEqual(false, state);
            Assert.AreEqual(3, handler.Count);

            state = Reprocess.Run(() => Console.WriteLine("Hello, World!"));
            Assert.AreEqual(true, state);
            Assert.AreEqual(3, handler.Count);
        }

        [TestMethod]
        public void RunActionAsync()
        {
            handler.Count = 0;

            bool state = Reprocess.RunAsync(async () =>
            {
                await Task.Delay(50);
                throw new InvalidOperationException();
            }).Result;
            Assert.AreEqual(false, state);
            Assert.AreEqual(3, handler.Count);

            state = Reprocess.RunAsync(async () =>
            {
                await Task.Delay(50);
                Console.WriteLine("Hello, World!");
            }).Result;
            Assert.AreEqual(true, state);
            Assert.AreEqual(3, handler.Count);
        }

        [TestMethod]
        public void RunActionFailFailSuccess()
        {
            handler.Count = 0;

            bool state = Reprocess.Run(() =>
            {
                if (handler.Count < 2)
                    throw new InvalidOperationException();
            });
            Assert.AreEqual(true, state);
            Assert.AreEqual(2, handler.Count);
        }

        [TestMethod]
        public void RunFunc()
        {
            handler.Count = 0;

            string result;
            bool state = Reprocess.Run(() => "Hello, World!", out result);
            Assert.AreEqual(true, state);
            Assert.AreEqual("Hello, World!", result);
            Assert.AreEqual(0, handler.Count);

            state = Reprocess.Run(() => throw new InvalidOperationException(), out result);
            Assert.AreEqual(false, state);
            Assert.AreEqual(default(string), result);
            Assert.AreEqual(3, handler.Count);
        }

        private string ThrowException() => throw new InvalidOperationException();
        private async Task<string> ThrowExceptionAsync()
        {
            await Task.Delay(50);
            throw new InvalidOperationException();
        }

        [TestMethod]
        public void RunFuncWithReturnValue()
        {
            handler.Count = 0;

            string result = Reprocess.Run(() => "Hello, World!");
            Assert.AreEqual("Hello, World!", result);
            Assert.AreEqual(0, handler.Count);

            result = Reprocess.Run(ThrowException);
            Assert.AreEqual(default(string), result);
            Assert.AreEqual(3, handler.Count);
        }

        [TestMethod]
        public void RunFuncWithReturnValueAsync()
        {
            handler.Count = 0;

            Func<Task<string>> execute = async () =>
            {
                await Task.Delay(50);
                return "Hello, World!";
            };

            string result = Reprocess.RunAsync(execute).Result;
            Assert.AreEqual("Hello, World!", result);
            Assert.AreEqual(0, handler.Count);

            result = Reprocess.RunAsync(ThrowExceptionAsync).Result;
            Assert.AreEqual(default(string), result);
            Assert.AreEqual(3, handler.Count);
        }
    }
}
