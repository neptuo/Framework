using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Components
{
    /// <summary>
    /// The factory helper for creating envelopes.
    /// </summary>
    public static class Envelope
    {
        /// <summary>
        /// Creates new instance of <see cref="Envelope{T}"/> with body.
        /// </summary>
        /// <typeparam name="T">Type of the body.</typeparam>
        /// <param name="body">The body of the evelope.</param>
        /// <returns>New instance of <see cref="Envelope{T}"/> with <paramref name="body"/>.</returns>
        public static Envelope<T> Create<T>(T body)
        {
            return new Envelope<T>(body);
        }
    }

    /// <summary>
    /// The evelope for processing long running operations.
    /// Provides access to common metadata.
    /// </summary>
    /// <typeparam name="T">The type of envelope body.</typeparam>
    public class Envelope<T>
    {
        /// <summary>
        /// The body of the evelope.
        /// </summary>
        public T Body { get; private set; }

        /// <summary>
        /// The delay for sending, enqueing or processing the body.
        /// </summary>
        public TimeSpan Delay { get; private set; }

        /// <summary>
        /// The time to live for the message in the queue.
        /// If not specified, value is <see cref="TimeSpan.Zero"/>.
        /// </summary>
        public TimeSpan TimeToLive { get; private set; }

        /// <summary>
        /// The source message/envelope ID.
        /// </summary>
        public string SourceID { get; private set; }

        /// <summary>
        /// Creates new instance of <see cref="Envelope{T}"/> with specified parameters.
        /// </summary>
        /// <param name="body">The body of the evelope.</param>
        /// <param name="delay">The delay for sending, enqueing or processing the body.</param>
        /// <param name="timeToLive">The time to live for the message in the queue.</param>
        /// <param name="sourceID">The source message/envelope ID.</param>
        public Envelope(T body, TimeSpan delay, TimeSpan timeToLive, string sourceID)
            : this(body, delay, timeToLive)
        {
            SourceID = sourceID;
        }

        /// <summary>
        /// Creates new instance of <see cref="Envelope{T}"/> with specified parameters.
        /// </summary>
        /// <param name="body">The body of the evelope.</param>
        /// <param name="delay">The delay for sending, enqueing or processing the body.</param>
        /// <param name="timeToLive">The time to live for the message in the queue.</param>
        public Envelope(T body, TimeSpan delay, TimeSpan timeToLive)
        {
            Ensure.NotNull(body, "body");
            Ensure.NotNull(delay, "delay");
            Ensure.NotNull(timeToLive, "timeToLive");
            Body = body;
            Delay = delay;
            TimeToLive = timeToLive;
            SourceID = null;
        }

        /// <summary>
        /// Creates new instance of <see cref="Envelope{T}"/> with body and source ID.
        /// </summary>
        /// <param name="body">The body of the evelope.</param>
        /// <param name="sourceID">The source message/envelope ID.</param>
        public Envelope(T body, string sourceID)
            : this(body, TimeSpan.Zero, TimeSpan.Zero, sourceID)
        { }

        /// <summary>
        /// Creates new instance of <see cref="Envelope{T}"/> with body.
        /// </summary>
        /// <param name="body">The body of the evelope.</param>
        public Envelope(T body)
            : this(body, TimeSpan.Zero, TimeSpan.Zero)
        { }
    }
}
