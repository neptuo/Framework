using Neptuo;
using Neptuo.Exceptions.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Threading.Tasks
{
    /// <summary>
    /// A common extensions for handling exception in tasks.
    /// </summary>
    public static class _TaskExtensions
    {
        /// <summary>
        /// Awaits on <paramref name="task"/> and uses <paramref name="handler"/> to handle any exceptions.
        /// </summary>
        /// <param name="task">A task to await.</param>
        /// <param name="handler">A handler to use when exception raises.</param>
        /// <returns>A continuation task from <paramref name="task"/>.</returns>
        public static async Task HandleException(this Task task, IExceptionHandler handler)
        {
            Ensure.NotNull(task, "task");

            try
            {
                await task;
            }
            catch (Exception e)
            {
                handler.Handle(e);
            }
        }
    }
}
