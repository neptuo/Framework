using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Behaviors.Processing
{
    /// <summary>
    /// Defines executable pipeline for inner handler of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">Type of inner handler.</typeparam>
    public interface IPipeline<T>
    {
        /// <summary>
        /// Adds <paramref name="behavior"/> to the pipeline.
        /// </summary>
        /// <param name="position">Position where <paramref name="behavior"/> should be executed.</param>
        /// <param name="behavior">Instance of behavior.</param>
        /// <returns>Self (for fluency).</returns>
        IPipeline<T> AddBehavior(PipelineBehaviorPosition position, IBehavior<T> behavior);

        /// <summary>
        /// Executes pipeline on <paramref name="handler"/>.
        /// </summary>
        /// <param name="handler">Handler instance to execute pipeline on.</param>
        /// <param name="customValues">Collection of custom values passed around invokation.</param>
        /// <returns>Continuation task.</returns>
        Task ExecuteAsync(T handler, IKeyValueCollection customValues);
    }
}
