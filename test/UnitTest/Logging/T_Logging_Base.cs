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
        protected ILogFactory CreateLogFactory(out StringSerializer writer, LogLevel minLevel = LogLevel.Debug)
        {
            writer = new StringSerializer((scopeName, level) => level >= minLevel);
            return new DefaultLogFactory("Root")
                .AddSerializer(writer);
        }

        [TestMethod]
        public void BaseComposition()
        {
            ILogFactory logFactory = new DefaultLogFactory();
            logFactory.AddConsole();

            ILog log = logFactory.Scope("Application");
            log.Debug("Hello, {0}!", "World");
        }

        [TestMethod]
        public void ConcatingEnumerationOfWriters()
        {
            StringSerializer[] w = new StringSerializer[] 
            {
                new StringSerializer(),
                new StringSerializer(),
                new StringSerializer(),
                new StringSerializer(),
                new StringSerializer()
            };

            ILogFactory logFactory = new DefaultLogFactory();
            logFactory.AddSerializer(w[0]);

            ILog l1 = logFactory.Scope("L1");
            l1.Debug("M1");
            EnsureMessageCount(w, 1, 0, 0, 0, 0);

            l1.Factory.AddSerializer(w[1]);
            l1.Debug("M3");
            EnsureMessageCount(w, 2, 0, 0, 0, 0);

            ILog l2 = l1.Factory.Scope("L2");
            l2.Debug("M3");
            EnsureMessageCount(w, 3, 1, 0, 0, 0);

            l2.Factory.AddSerializer(w[2]);
            l2.Debug("M4");
            EnsureMessageCount(w, 4, 2, 0, 0, 0);

            ILog l3 = l2.Factory.Scope("L3");
            l3.Debug("M5");
            EnsureMessageCount(w, 5, 3, 1, 0, 0);

            l1.Factory.AddSerializer(w[3]);
            ILog l4 = l1.Factory.Scope("L4");
            l4.Debug("M6");
            EnsureMessageCount(w, 6, 4, 1, 1, 0);

            l4.Factory.AddSerializer(w[4]);
            ILog l5 = l4.Factory.Scope("L5");
            l4.Debug("M7");
            EnsureMessageCount(w, 7, 5, 1, 2, 0);

            l5.Debug("M8");
            EnsureMessageCount(w, 8, 6, 1, 3, 1);
        }

        private void EnsureMessageCount(StringSerializer[] writers, params int[] counts)
        {
            Assert.AreEqual(counts.Length, writers.Length);

            for (int i = 0; i < writers.Length; i++)
                Assert.AreEqual(counts[i], writers[i].Messages.Count);
        }

        [TestMethod]
        public void Scopes()
        {
            StringSerializer writer;
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

        [TestMethod]
        public void LoggingExtensionMethods()
        {
            StringSerializer writer;
            ILogFactory logFactory = CreateLogFactory(out writer);
            ILog log = logFactory.Scope("Application");

            log.Debug("M1");
            EnsureMessage(writer, 0, "Root.Application", LogLevel.Debug, "M1");
            log.Debug("M{0}", 2);
            EnsureMessage(writer, 1, "Root.Application", LogLevel.Debug, "M2");

            log.Info("M3");
            EnsureMessage(writer, 2, "Root.Application", LogLevel.Info, "M3");
            log.Info("M{0}", 4);
            EnsureMessage(writer, 3, "Root.Application", LogLevel.Info, "M4");

            log.Warning("M5");
            EnsureMessage(writer, 4, "Root.Application", LogLevel.Warning, "M5");
            log.Warning("M{0}", 6);
            EnsureMessage(writer, 5, "Root.Application", LogLevel.Warning, "M6");

            log.Error("M7");
            EnsureMessage(writer, 6, "Root.Application", LogLevel.Error, "M7");
            log.Error("M{0}", 8);
            EnsureMessage(writer, 7, "Root.Application", LogLevel.Error, "M8");
            NotImplementedException e1 = Ensure.Exception.NotImplemented();
            log.Error(e1);
            EnsureMessage(writer, 8, "Root.Application", LogLevel.Error, e1);
            NotImplementedException e2 = Ensure.Exception.NotImplemented();
            log.Error(e2, "M9");
            EnsureMessage(writer, 9, "Root.Application", LogLevel.Error, new ExceptionModel("M9", e2));
            NotImplementedException e3 = Ensure.Exception.NotImplemented();
            log.Error(e3, "M{0}", 10);
            EnsureMessage(writer, 10, "Root.Application", LogLevel.Error, new ExceptionModel("M10", e3));

            log.Fatal("M11");
            EnsureMessage(writer, 11, "Root.Application", LogLevel.Fatal, "M11");
            log.Fatal("M{0}", 12);
            EnsureMessage(writer, 12, "Root.Application", LogLevel.Fatal, "M12");
            NotImplementedException e4 = Ensure.Exception.NotImplemented();
            log.Fatal(e4);
            EnsureMessage(writer, 13, "Root.Application", LogLevel.Fatal, e4);
            NotImplementedException e5 = Ensure.Exception.NotImplemented();
            log.Fatal(e5, "M13");
            EnsureMessage(writer, 14, "Root.Application", LogLevel.Fatal, new ExceptionModel("M13", e5));
            NotImplementedException e6 = Ensure.Exception.NotImplemented();
            log.Fatal(e6, "M{0}", 14);
            EnsureMessage(writer, 15, "Root.Application", LogLevel.Fatal, new ExceptionModel("M14", e6));
        }

        private void EnsureMessage(StringSerializer writer, int index, string scopeName, LogLevel level, object messageModel)
        {
            StringLogMessage message = writer.Messages[index];
            Assert.AreEqual(scopeName, message.ScopeName);
            Assert.AreEqual(level, message.Level);
            Assert.IsInstanceOfType(message.Model, messageModel.GetType());
            Assert.AreEqual(messageModel, message.Model);
        }
    }
}
