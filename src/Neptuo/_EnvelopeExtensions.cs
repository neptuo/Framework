using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo
{
    /// <summary>
    /// Common extensions for <see cref="Envelope{T}"/>.
    /// </summary>
    public static class _EnvelopeExtensions
    {
        public static Envelope<T> AddTimeToLive<T>(this Envelope<T> envelope, TimeSpan timeToLive)
        {
            Ensure.NotNull(envelope, "envelope");
            envelope.Metadata.Add("TimeToLive", timeToLive);
            return envelope;
        }

        public static bool TryGetTimeToLive<T>(this Envelope<T> envelope, out TimeSpan timeToLive)
        {
            Ensure.NotNull(envelope, "envelope");
            return envelope.Metadata.TryGet("TimeToLive", out timeToLive);
        }


        public static Envelope<T> AddDelay<T>(this Envelope<T> envelope, TimeSpan delay)
        {
            Ensure.NotNull(envelope, "envelope");
            envelope.Metadata.Add("Delay", delay);
            return envelope;
        }

        public static bool TryGetDelay<T>(this Envelope<T> envelope, out TimeSpan delay)
        {
            Ensure.NotNull(envelope, "envelope");
            return envelope.Metadata.TryGet("Delay", out delay);
        }
    }
}
