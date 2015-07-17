using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Logging
{
    [TestClass]
    public class T_Logging_Base
    {
        protected ILogFactory CreateLogFactory(out StringLogWriter writer, LogLevel minLevel = LogLevel.Debug)
        {
            writer = new StringLogWriter(level => level >= minLevel);
            return new DefaultLogFactory("Root")
                .AddWriter(writer);
        }

        [TestMethod]
        public void BaseComposition()
        {
            ILogFactory logFactory = new DefaultLogFactory();
            logFactory.AddConsoleWriter();

            ILog log = logFactory.Scope("Application");
            log.Debug("Hello, {0}!", "World");
        }

        [TestMethod]
        public void Scopes()
        {
            StringLogWriter writer;
            ILogFactory logFactory = CreateLogFactory(out writer);

            ILog applicationLog = logFactory.Scope("Application");
            applicationLog.Debug("Hello, World");
            
            Assert.AreEqual(1, writer.Messages.Count);
            Assert.AreEqual("Root.Application", writer.Messages[0].ScopeName);

            ILog app2Log = logFactory.Scope("App2");
            app2Log.Debug("App2");

            Assert.AreEqual(2, writer.Messages.Count);
            Assert.AreEqual("Root.App2", writer.Messages[1].ScopeName);

            ILog bootLog = applicationLog.Factory.Scope("Bootstrap");
            bootLog.Debug("Boot1");

            Assert.AreEqual(3, writer.Messages.Count);
            Assert.AreEqual("Root.Application.Bootstrap", writer.Messages[2].ScopeName);
        }
    }
}
