using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptuo.Observables.Commands;
using Neptuo.Observables.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Neptuo.Observables
{
    [TestClass]
    public class TestAsyncCommand
    {
        [TestMethod]
        public void Execution()
        {
            DelayAsyncCommand mainCommand = new DelayAsyncCommand();

            mainCommand.Execute();
            Assert.AreEqual(true, mainCommand.IsRunning);
            Assert.AreEqual(true, mainCommand.IsPhase1Reached);

            Thread.Sleep(10000);

            Assert.AreEqual(false, mainCommand.IsRunning);
            Assert.AreEqual(true, mainCommand.IsPhase2Reached);
        }

        [TestMethod]
        public void Cancellation()
        {
            DelayAsyncCommand mainCommand = new DelayAsyncCommand();
            CancelCommand cancelCommand = new CancelCommand(mainCommand);

            mainCommand.Execute();
            Assert.AreEqual(true, mainCommand.IsRunning);
            Assert.AreEqual(true, mainCommand.IsPhase1Reached);

            cancelCommand.Execute();
            Thread.Sleep(10000);

            Assert.AreEqual(false, mainCommand.IsRunning);
            Assert.AreEqual(false, mainCommand.IsPhase2Reached);
        }
    }
}
