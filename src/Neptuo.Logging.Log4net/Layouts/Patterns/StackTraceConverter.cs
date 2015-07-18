using log4net;
using log4net.Core;
using log4net.Layout.Pattern;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Logging.Layouts.Patterns
{
    /// <summary>
    /// Extension of <see cref="PatternLayoutConverter"/> that reads actual call stack.
    /// </summary>
    public class StackTraceConverter : PatternLayoutConverter
    {
        protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
        {
            LocationInfo locationInfo = loggingEvent.LocationInformation;
            var stack = new StackTrace(true);
            var writeFrame = false;

            StackFrame[] frames = stack.GetFrames();
            if (frames == null)
                return;

            foreach (StackFrame frame in frames)
            {
                MethodBase method = frame.GetMethod();
                int lineNumber = frame.GetFileLineNumber();

                if (!writeFrame)
                {
                    if (method.DeclaringType != null
                        && (method.DeclaringType.FullName == locationInfo.ClassName
                            && method.Name == locationInfo.MethodName
                            && lineNumber.ToString() == locationInfo.LineNumber))

                        writeFrame = true;
                }

                if (!writeFrame)
                    continue;

                if (!String.IsNullOrEmpty(frame.GetFileName()))
                {
                    if (method.DeclaringType != null)
                    {
                        writer.WriteLine("at {0}.{1}({2}:{3})",
                            method.DeclaringType.FullName,
                            method.Name,
                            frame.GetFileName(),
                            frame.GetFileLineNumber());
                    }
                }
            }
        }
    }
}
