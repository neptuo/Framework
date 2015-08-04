using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Behaviors.Processing
{
    /// <summary>
    /// Common extensions for <see cref="IPipeline{T}"/>.
    /// </summary>
    public static class _PipelineExtensions
    {
        /// <summary>
        /// Executes pipeline on <paramref name="handler"/> wiht custom values.
        /// </summary>
        /// <param name="handler">Handler instance to execute pipeline on.</param>
        /// <returns>Continuation task.</returns>
        public static Task ExecuteAsync<T>(this IPipeline<T> pipeline, T handler)
        {
            Ensure.NotNull(pipeline, "pipeline");
            return pipeline.ExecuteAsync(handler, new KeyValueCollection());
        }
    }
}
