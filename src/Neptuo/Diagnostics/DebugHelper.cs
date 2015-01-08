using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Diagnostics
{
    /// <summary>
    /// Provides access for method execution in stopwatch.
    /// Output is always written to <see cref="Console.Out"/>.
    /// </summary>
    public class DebugHelper
    {
        private static object debugLock = new object();
        private static DebugBase debug = null;

        private static void EnsureDebug()
        {
            if (debug == null)
            {
                lock (debugLock)
                {
                    if (debug == null)
                        debug = new DebugBase(Console.Out);
                }
            }
        }

        /// <summary>
        /// Executes <paramref name="action"/> in stopwatch and saves message titled as <paramref name="title"/>.
        /// </summary>
        /// <param name="title">Measurement title.</param>
        /// <param name="action">Action to execute in stopwatch.</param>
        public static void Debug(string title, Action action)
        {
            EnsureDebug();
            debug.Debug(title, action);
        }

        /// <summary>
        /// Executes <paramref name="action"/> <paramref name="count"/>-times in stopwatch and saves message titled as <paramref name="title"/>.
        /// </summary>
        /// <param name="title">Measurement title.</param>
        /// <param name="count">Number of exections to run.</param>
        /// <param name="action">Action to execute in stopwatch.</param>
        public static void DebugIteration(string title, int count, Action action)
        {
            EnsureDebug();
            debug.DebugIteration(title, count, action);
        }

        /// <summary>
        /// Executes <paramref name="action"/> in stopwatch and saves message titled as <paramref name="title"/>.
        /// </summary>
        /// <typeparam name="T">Type returned from <paramref name="action"/>.</typeparam>
        /// <param name="title">Measurement title.</param>
        /// <param name="action">Action to execute in stopwatch.</param>
        /// <rereturns>Returns result from <paramref name="action"/>.</rereturns>
        public static T Debug<T>(string title, Func<T> action)
        {
            EnsureDebug();
            return debug.Debug(title, action);
        }

        /// <summary>
        /// Executes <paramref name="action"/> <paramref name="count"/>-times in stopwatch and saves message titled as <paramref name="title"/>.
        /// </summary>
        /// <typeparam name="T">Type returned from <paramref name="action"/>.</typeparam>
        /// <param name="title">Measurement title.</param>
        /// <param name="count">Number of exections to run.</param>
        /// <param name="action">Action to execute in stopwatch.</param>
        /// <rereturns>Returns result from <paramref name="action"/>.</rereturns>
        public static List<T> DebugIteration<T>(string title, int count, Func<T> action)
        {
            EnsureDebug();
            return debug.DebugIteration(title, count, action);
        }
    }
}
