using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptuo.Observables.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Neptuo.Observables
{
    [TestClass]
    public class T_Observables_AsyncCommand
    {
        [TestMethod]
        public void Cancellations()
        {
            TestAsyncCommand mainCommand = new TestAsyncCommand();
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
