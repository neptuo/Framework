using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptuo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Neptuo
{
    [TestClass]
    public class T_SchedulingContext
    {
        private class SchedulingContext : ISchedulingContext
        {
            public Envelope Envelope { get; private set; }
            public bool IsExecuted { get; private set; }

            public SchedulingContext(Envelope envelope)
            {
                Ensure.NotNull(envelope, "envelope");
                Envelope = envelope;
            }

            public void Execute()
            {
                if (IsExecuted)
                    Assert.Fail("SchedulingContext already executed.");

                IsExecuted = true;
            }
        }

        [TestMethod]
        public void Collection()
        {
            TimerSchedulingProvider provider = new TimerSchedulingProvider(new TimerSchedulingProvider.DateTimeNowProvider());

            Envelope envelope1 = Envelope.Create("Hello, World!")
                .AddExecuteAt(DateTime.Now.AddDays(10));

            Envelope envelope2 = Envelope.Create("Hi, there!")
                .AddExecuteAt(DateTime.Now.AddMinutes(1));

            SchedulingContext context1 = new SchedulingContext(envelope1);
            SchedulingContext context2 = new SchedulingContext(envelope2);

            provider.Add(context1);
            provider.Add(context2);

            Assert.AreEqual(true, provider.IsContained(context1));
            Assert.AreEqual(false, context1.IsExecuted);
            Assert.AreEqual(true, provider.IsContained(context2));
            Assert.AreEqual(false, context2.IsExecuted);

            provider.Remove(context1);

            Assert.AreEqual(false, provider.IsContained(context1));
            Assert.AreEqual(false, context1.IsExecuted);
            Assert.AreEqual(true, provider.IsContained(context2));
            Assert.AreEqual(false, context2.IsExecuted);

            provider.Remove(context2);

            Assert.AreEqual(false, provider.IsContained(context1));
            Assert.AreEqual(false, context1.IsExecuted);
            Assert.AreEqual(false, provider.IsContained(context2));
            Assert.AreEqual(false, context2.IsExecuted);

            provider.Add(context1);

            Assert.AreEqual(true, provider.IsContained(context1));
            Assert.AreEqual(false, context1.IsExecuted);
            Assert.AreEqual(false, provider.IsContained(context2));
            Assert.AreEqual(false, context2.IsExecuted);

            provider.Add(context2);

            Assert.AreEqual(true, provider.IsContained(context1));
            Assert.AreEqual(false, context1.IsExecuted);
            Assert.AreEqual(true, provider.IsContained(context2));
            Assert.AreEqual(false, context2.IsExecuted);

            foreach (ISchedulingContext item in provider.Enumerate())
            {
                if (item.Equals(context1))
                {
                    item.Execute();

                    Assert.AreEqual(false, provider.IsContained(context1));
                    Assert.AreEqual(true, context1.IsExecuted);
                }
                else if (item.Equals(context2))
                {
                    item.Execute();

                    Assert.AreEqual(false, provider.IsContained(context2));
                    Assert.AreEqual(true, context2.IsExecuted);
                }
                else
                {
                    Assert.Fail("Collection contains other items.");
                }
            }

            Assert.AreEqual(false, provider.IsContained(context1));
            Assert.AreEqual(true, context1.IsExecuted);
            Assert.AreEqual(false, provider.IsContained(context2));
            Assert.AreEqual(true, context2.IsExecuted);
        }

        [TestMethod]
        public void LongRunner_3xPeriod()
        {
            TimerSchedulingProvider provider = new TimerSchedulingProvider(new TimerSchedulingProvider.DateTimeNowProvider(), TimeSpan.FromMinutes(1));

            Envelope envelope = Envelope.Create("Hello, World!")
                .AddExecuteAt(DateTime.Now.AddMinutes(3));

            SchedulingContext context = new SchedulingContext(envelope);
            provider.Add(context);

            Thread.Sleep(TimeSpan.FromSeconds(3 * 60 + 10));

            Assert.AreEqual(true, context.IsExecuted);
        }

        [TestMethod]
        public void LongRunner_1xPeriod()
        {
            TimerSchedulingProvider provider = new TimerSchedulingProvider(new TimerSchedulingProvider.DateTimeNowProvider(), TimeSpan.FromMinutes(1));

            Envelope envelope = Envelope.Create("Hello, World!")
                .AddExecuteAt(DateTime.Now.AddMinutes(1));

            SchedulingContext context = new SchedulingContext(envelope);
            provider.Add(context);

            Thread.Sleep(TimeSpan.FromSeconds(1 * 60 + 10));

            Assert.AreEqual(true, context.IsExecuted);
        }
    }
}
