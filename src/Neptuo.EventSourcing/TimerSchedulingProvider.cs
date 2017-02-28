using Neptuo;
using Neptuo.Commands;
using Neptuo.Internals;
using Neptuo.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Neptuo
{
    /// <summary>
    /// The implentation of <see cref="ISchedulingProvider"/> based on the <see cref="Timer"/>.
    /// When scheduling context should be executed after <see cref="longRunnerThreshold"/>, it uses single timer
    /// to check the queue every <see cref="longRunnerThreshold"/> and re-evaluate if the contexts should be scheduled.
    /// </summary>
    public partial class TimerSchedulingProvider : ISchedulingProvider, ISchedulingCollection
    {
        private readonly IDateTimeProvider dateTimeProvider;
        private readonly ILog log;

        private readonly object timersLock = new object();
        private readonly List<Tuple<Timer, ISchedulingContext>> timers = new List<Tuple<Timer, ISchedulingContext>>();

        private readonly TimeSpan longRunnerThreshold;
        private readonly object longRunnersLock = new object();
        private List<ISchedulingContext> longRunners = null;
        private Timer longRunnerTimer;

        /// <summary>
        /// Creates new instance that uses <c>1d</c> to threshold 'long runnner' contexts.
        /// </summary>
        /// <param name="dateTimeProvider">The provider of execution delay.</param>
        public TimerSchedulingProvider(IDateTimeProvider dateTimeProvider)
            : this(dateTimeProvider, TimeSpan.FromDays(1), new DefaultLogFactory())
        { }

        /// <summary>
        /// Creates new instance that uses <c>1d</c> to threshold 'long runnner' contexts.
        /// </summary>
        /// <param name="dateTimeProvider">The provider of execution delay.</param>
        /// <param name="logFactory">A log factory.</param>
        public TimerSchedulingProvider(IDateTimeProvider dateTimeProvider, ILogFactory logFactory)
            : this(dateTimeProvider, TimeSpan.FromDays(1), logFactory)
        { }

        /// <summary>
        /// Creates new instance that uses <paramref name="longRunnerThreshold"/> to threshold 'long runnner' contexts. 
        /// </summary>
        /// <param name="dateTimeProvider">The provider of execution delay.</param>
        /// <param name="longRunnerThreshold">The timespan to be used as threshold when deciding whether the context is 'long runner'.</param>
        public TimerSchedulingProvider(IDateTimeProvider dateTimeProvider, TimeSpan longRunnerThreshold)
            : this(dateTimeProvider, longRunnerThreshold, new DefaultLogFactory())
        { }

        /// <summary>
        /// Creates new instance that uses <paramref name="longRunnerThreshold"/> to threshold 'long runnner' contexts. 
        /// </summary>
        /// <param name="dateTimeProvider">The provider of execution delay.</param>
        /// <param name="longRunnerThreshold">The timespan to be used as threshold when deciding whether the context is 'long runner'.</param>
        /// <param name="logFactory">A log factory.</param>
        public TimerSchedulingProvider(IDateTimeProvider dateTimeProvider, TimeSpan longRunnerThreshold, ILogFactory logFactory)
        {
            Ensure.NotNull(dateTimeProvider, "dateTimeProvider");
            EnsureThreshold(longRunnerThreshold, "longRunnerThreshold");
            Ensure.NotNull(logFactory, "logFactory");
            this.dateTimeProvider = dateTimeProvider;
            this.longRunnerThreshold = longRunnerThreshold;
            this.log = logFactory.Scope("TimerSchedulingProvider");
        }

        private static void EnsureThreshold(TimeSpan longRunnerThreshold, string parameterName)
        {
            if (longRunnerThreshold < TimeSpan.Zero)
                throw Ensure.Exception.ArgumentOutOfRange(parameterName, "Long runner threshold must be between '0ms' and '1day'.");
        }

        public bool IsLaterExecutionRequired(Envelope envelope)
        {
            if (envelope == null)
            {
                log.Debug("Got null envelope.");
                return false;
            }

            DateTime executeAt;
            if (!envelope.TryGetExecuteAt(out executeAt))
            {
                log.Debug(envelope, "Got envelope without 'ExecuteAt' metadata.");
                return false;
            }

            TimeSpan delay = dateTimeProvider.GetExecutionDelay(executeAt);
            log.Debug(envelope, "Got envelope with current delay '{0}'.", delay);

            return delay > TimeSpan.Zero;
        }

        public void Schedule(ISchedulingContext context)
        {
            Ensure.NotNull(context, "context");

            DateTime executeAt;
            if (context.Envelope.TryGetExecuteAt(out executeAt))
            {
                TimeSpan delay = dateTimeProvider.GetExecutionDelay(executeAt);
                if (delay > longRunnerThreshold)
                {
                    log.Info(context.Envelope, "Got envelope for long runner with current delay '{0}'.", delay);
                    ScheduleLongRunner(context);
                }
                else if (delay > TimeSpan.Zero)
                {
                    log.Info(context.Envelope, "Creating timer with current delay '{0}'.", delay);
                    Timer timer = new Timer(
                        OnScheduled,
                        context,
                        delay,
                        TimeSpan.FromMilliseconds(-1)
                    );

                    lock (timersLock)
                        timers.Add(new Tuple<Timer, ISchedulingContext>(timer, context));

                    log.Debug(context.Envelope, "Timer registered.");
                }
                else
                {
                    // This should be handled by the calling infrastructure.
                    log.Info(context.Envelope, "Got envelope with current delay in past '{0}'. Executing.", delay);
                    context.Execute();
                }
            }
            else
            {
                // This should be handled by the calling infrastructure.
                log.Info(context.Envelope, "Got envelope without 'ExecuteAt' metadata. Executing.");
                context.Execute();
            }
        }

        /// <summary>
        /// When the context should be executed and removed from the <see cref="timers"/>.
        /// </summary>
        /// <param name="state">The context to be executed.</param>
        private void OnScheduled(object state)
        {
            ISchedulingContext context = (ISchedulingContext)state;

            log.Debug(context.Envelope, "Envelope timer callback raised.");

            Remove(context);

            log.Info(context.Envelope, "Executing.");

            context.Execute();

            log.Debug(context.Envelope, "Envelope timer callback ended.");
        }

        /// <summary>
        /// Enqueues <paramref name="context"/> to be scheduled as 'long runnner'.
        /// </summary>
        /// <param name="context">The context to be scheduled as 'long runner'</param>
        private void ScheduleLongRunner(ISchedulingContext context)
        {
            if (longRunners == null)
            {
                log.Debug(context.Envelope, "Long runner collection is null. Creating the collection.");

                lock (longRunnersLock)
                {
                    if (longRunners == null)
                        longRunners = new List<ISchedulingContext>();

                    if (longRunnerTimer == null)
                    {
                        log.Debug(context.Envelope, "Long runner timer is null. Creating the timer.");

                        longRunnerTimer = new Timer(
                            OnLongRunnerScheduled,
                            null,
                            longRunnerThreshold,
                            longRunnerThreshold
                        );
                    }

                    log.Info(context.Envelope, "Added as a long runner.");
                    longRunners.Add(context);
                }
            }
            else
            {
                lock (longRunnersLock)
                    longRunners.Add(context);

                log.Info(context.Envelope, "Added as a long runner.");
            }
        }

        /// <summary>
        /// Checks <see cref="longRunners"/> and re-evealuates if the contexts should be scheduled using regular timers.
        /// </summary>
        private void OnLongRunnerScheduled(object state)
        {
            log.Debug("Long runner timer raised.");

            List<ISchedulingContext> toSchedule = new List<ISchedulingContext>();
            lock (longRunnersLock)
            {
                if (longRunners == null)
                {
                    log.Debug("Long runner collection is null. Disposing the timer.");

                    longRunnerTimer.Dispose();
                    longRunnerTimer = null;
                    return;
                }

                foreach (ISchedulingContext context in longRunners)
                {
                    DateTime executeAt;
                    if (context.Envelope.TryGetExecuteAt(out executeAt))
                    {
                        TimeSpan delay = dateTimeProvider.GetExecutionDelay(executeAt);
                        log.Debug(context.Envelope, "Long runner envelope with current delay '{0}'.", delay);

                        if (delay < longRunnerThreshold)
                        {
                            toSchedule.Add(context);
                            log.Debug(context.Envelope, "Added to a collection for timer execution.");
                        }
                    }
                }

                foreach (ISchedulingContext context in toSchedule)
                    RemoveLongRunnerUnsafe(context);
            }

            foreach (ISchedulingContext context in toSchedule)
                Schedule(context);
        }

        public IEnumerable<ISchedulingContext> Enumerate()
        {
            List<ISchedulingContext> result = new List<ISchedulingContext>();

            lock (timersLock)
            {
                foreach (Tuple<Timer, ISchedulingContext> item in timers)
                    result.Add(new SchedulingContext(this, item.Item2));
            }

            if (longRunners == null)
                return result;

            lock (longRunnersLock)
            {
                if (longRunners == null)
                    return result;

                foreach (ISchedulingContext longRunner in longRunners)
                    result.Add(new SchedulingContext(this, longRunner));
            }

            return result;
        }

        public ISchedulingCollection Add(ISchedulingContext context)
        {
            Ensure.NotNull(context, "context");
            if (IsLaterExecutionRequired(context.Envelope))
                Schedule(context);

            return this;
        }

        public ISchedulingCollection Remove(ISchedulingContext context)
        {
            Ensure.NotNull(context, "context");

            lock (timersLock)
            {
                Tuple<Timer, ISchedulingContext> item = timers.FirstOrDefault(t => t.Item2.Equals(context));
                if (item != null)
                {
                    log.Info(context.Envelope, "Found timer. Disposing the timer and removing from the collection.");

                    item.Item1.Dispose();
                    timers.Remove(item);
                    return this;
                }
            }

            log.Debug(context.Envelope, "Timer not found.");

            if (longRunners == null)
            {
                log.Debug(context.Envelope, "Long runners are null. Returning.");
                return this;
            }

            lock (longRunnersLock)
            {
                if (longRunners == null)
                {
                    log.Debug(context.Envelope, "Long runners are null. Returning.");
                    return this;
                }

                RemoveLongRunnerUnsafe(context);
            }

            return this;
        }

        private void RemoveLongRunnerUnsafe(ISchedulingContext context)
        {
            longRunners.Remove(context);
            log.Info(context.Envelope, "Removed from long runner collection.");

            if (longRunners.Count == 0)
            {
                longRunners = null;
                longRunnerTimer.Dispose();
                longRunnerTimer = null;

                log.Debug("Long runner collection is empty. Disposing a timer.");
            }
        }

        public bool IsContained(ISchedulingContext context)
        {
            Ensure.NotNull(context, "context");
            lock (timersLock)
            {
                foreach (Tuple<Timer, ISchedulingContext> item in timers)
                {
                    if (item.Item2.Equals(context))
                        return true;
                }
            }

            if (longRunners == null)
                return false;

            lock (longRunnersLock)
            {
                if (longRunners == null)
                    return false;

                foreach (ISchedulingContext longRunner in longRunners)
                {
                    if (longRunner.Equals(context))
                        return true;
                }
            }

            return false;
        }
    }
}
