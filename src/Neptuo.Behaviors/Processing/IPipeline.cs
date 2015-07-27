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
        IPipeline<T> AddBehavior()

        /// <summary>
        /// Executes pipeline on <paramref name="handler"/>.
        /// </summary>
        /// <param name="handler">Handler instance to execute pipeline on.</param>
        /// <returns>Continuation task.</returns>
        Task ExecuteAsync(T handler);
    }
}
