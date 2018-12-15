using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptuo.Observables.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Observables
{
    [TestClass]
    public class TestAsyncProgressCommand
    {
        [TestMethod]
        public void Parameterless()
        {
            List<int> progress = new List<int>();

            var command = new AsyncProgressCommand();
            command.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(AsyncProgressCommand.Progress))
                    progress.Add(command.Progress);
            };

            command.ExecuteAsync().Wait();
            Assert.AreEqual(3, progress.Count);
            Assert.AreEqual(1, progress[0]);
            Assert.AreEqual(2, progress[1]);
            Assert.AreEqual(3, progress[2]);
        }

        [TestMethod]
        public void WithParameter()
        {
            List<int> progress = new List<int>();

            var command = new AsyncProgressCommandWithParameter();
            command.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(AsyncProgressCommand.Progress))
                    progress.Add(command.Progress);
            };

            command.ExecuteAsync(DateTime.Now).Wait();
            Assert.AreEqual(3, progress.Count);
            Assert.AreEqual(1, progress[0]);
            Assert.AreEqual(2, progress[1]);
            Assert.AreEqual(3, progress[2]);
        }
    }
}
