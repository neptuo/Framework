using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Diagnostics
{
    /// <summary>
    /// Provides access for method execution in stopwatch.
    /// </summary>
    public class DebugBase
    {
        /// <summary>
        /// Debug message writer. 
        /// Should use <paramref name="format"/> as formatter for <paramref name="parameters"/>.
        /// </summary>
        /// <param name="format">The message.</param>
        /// <param name="parameters">Optional arguments/parameters for <paramref name="format"/>.</param>
        public delegate void DebugMessageWriter(string format, params object[] parameters);

        private DebugMessageWriter innerWriter;

        /// <summary>
        /// Writer for measurements.
        /// </summary>
        protected DebugMessageWriter InnerWriter
        {
            get { return innerWriter; }
            set
            {
                if (value != null)
                    innerWriter = value;
            }
        }

        /// <summary>
        /// Creates instance and uses <paramref name="innerWriter"/> as writer for measurements.
        /// </summary>
        /// <param name="innerWriter">Writer for measurements.</param>
        public DebugBase(DebugMessageWriter innerWriter)
        {
            Guard.NotNull(innerWriter, "innerWriter");
            InnerWriter = innerWriter;
        }

        /// <summary>
        /// Creates instance and uses <paramref name="innerWriter"/> as writer for measurements.
        /// </summary>
        /// <param name="innerWriter">Writer for measurements.</param>
        public DebugBase(TextWriter innerWriter)
        {
            Guard.NotNull(innerWriter, "innerWriter");
            InnerWriter = innerWriter.WriteLine;
        }

        /// <summary>
        /// Executes <paramref name="action"/> in stopwatch and saves message titled as <paramref name="title"/>.
        /// </summary>
        /// <param name="title">Measurement title.</param>
        /// <param name="action">Action to execute in stopwatch.</param>
        protected internal void Debug(string title, Action action)
        {
            Guard.NotNull(title, "title");
            Guard.NotNull(action, "action");

            Stopwatch sw = new Stopwatch();
            sw.Start();

            action();

            sw.Stop();
            InnerWriter("{0}: {1}ms", title, sw.ElapsedMilliseconds);
        }

        /// <summary>
        /// Executes <paramref name="action"/> <paramref name="count"/>-times in stopwatch and saves message titled as <paramref name="title"/>.
        /// </summary>
        /// <param name="title">Measurement title.</param>
        /// <param name="count">Number of exections to run.</param>
        /// <param name="action">Action to execute in stopwatch.</param>
        protected internal void DebugIteration(string title, int count, Action action)
        {
            Guard.NotNull(title, "title");
            Guard.PositiveOrZero(count, "count");
            Guard.NotNull(action, "action");

            Debug(title, () =>
            {
                for (int i = 0; i < count; i++)
                    action();
            });
        }

        /// <summary>
        /// Executes <paramref name="action"/> in stopwatch and saves message titled as <paramref name="title"/>.
        /// </summary>
        /// <typeparam name="T">Type returned from <paramref name="action"/>.</typeparam>
        /// <param name="title">Measurement title.</param>
        /// <param name="action">Action to execute in stopwatch.</param>
        /// <rereturns>Returns result from <paramref name="action"/>.</rereturns>
        protected internal T Debug<T>(string title, Func<T> action)
        {
            Guard.NotNull(title, "title");
            Guard.NotNull(action, "action");

            Stopwatch sw = new Stopwatch();
            sw.Start();

            T result = action();

            sw.Stop();
            InnerWriter("{0}: {1}ms", title, sw.ElapsedMilliseconds);
            return result;
        }

        /// <summary>
        /// Executes <paramref name="action"/> <paramref name="count"/>-times in stopwatch and saves message titled as <paramref name="title"/>.
        /// </summary>
        /// <typeparam name="T">Type returned from <paramref name="action"/>.</typeparam>
        /// <param name="title">Measurement title.</param>
        /// <param name="count">Number of exections to run.</param>
        /// <param name="action">Action to execute in stopwatch.</param>
        /// <rereturns>Returns result from <paramref name="action"/>.</rereturns>
        protected internal List<T> DebugIteration<T>(string title, int count, Func<T> action)
        {
            Guard.NotNull(title, "title");
            Guard.PositiveOrZero(count, "count");
            Guard.NotNull(action, "action");

            return Debug(title, () =>
            {
                List<T> result = new List<T>();
                for (int i = 0; i < count; i++)
                    action();

                return result;
            });
        }
    }
}
