using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Diagnostics
{
    public class DebugBase
    {
        protected TextWriter InnerWriter;

        public DebugBase(TextWriter innerWriter)
        {
            Guard.NotNull(innerWriter, "innerWriter");
            InnerWriter = innerWriter;
        }

        protected internal void Debug(string title, Action action)
        {
            Guard.NotNull(title, "title");
            Guard.NotNull(action, "action");

            Stopwatch sw = new Stopwatch();
            sw.Start();

            action();

            sw.Stop();
            InnerWriter.WriteLine("{0}: {1}ms", title, sw.ElapsedMilliseconds);
        }

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

        protected internal T Debug<T>(string title, Func<T> action)
        {
            Guard.NotNull(title, "title");
            Guard.NotNull(action, "action");

            Stopwatch sw = new Stopwatch();
            sw.Start();

            T result = action();

            sw.Stop();
            InnerWriter.WriteLine("{0}: {1}ms", title, sw.ElapsedMilliseconds);
            return result;
        }

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
