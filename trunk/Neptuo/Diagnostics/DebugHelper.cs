using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Diagnostics
{
    public class DebugHelper
    {
        private static DebugBase debug = null;

        private static void EnsureDebug()
        {
            if (debug == null)
            {
                lock (debug)
                {
                    if (debug == null)
                        debug = new DebugBase(Console.Out);
                }
            }
        }

        public static void Debug(string title, Action action)
        {
            EnsureDebug();
            debug.Debug(title, action);
        }

        public static void DebugIteration(string title, int count, Action action)
        {
            EnsureDebug();
            debug.DebugIteration(title, count, action);
        }

        public static T Debug<T>(string title, Func<T> action)
        {
            EnsureDebug();
            return debug.Debug(title, action);
        }

        public static List<T> DebugIteration<T>(string title, int count, Func<T> action)
        {
            EnsureDebug();
            return debug.DebugIteration(title, count, action);
        }
    }
}
