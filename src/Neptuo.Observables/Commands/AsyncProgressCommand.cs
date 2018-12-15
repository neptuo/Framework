using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Neptuo.Observables.Commands
{
    /// <summary>
    /// A command reporting progress of type <typeparamref name="TProgress"/>.
    /// </summary>
    /// <typeparam name="TProgress">A type of the progress.</typeparam>
    public abstract class AsyncProgressCommand<TProgress> : AsyncCommand
    {
        private TProgress progress;

        /// <summary>
        /// Gets or sets current progress.
        /// </summary>
        public TProgress Progress
        {
            get { return progress; }
            protected set
            {
                if (!Equals(progress, value))
                {
                    progress = value;
                    RaisePropertyChanged();
                }
            }
        }

        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var progress = new ProgressManager(this);
            return ExecuteAsync(progress, cancellationToken);
        }

        /// <summary>
        /// Executes asynchronous operation.
        /// </summary>
        /// <param name="progress">A progress reporter.</param>
        /// <param name="cancellationToken">A token to indicate a cancellation request.</param>
        /// <returns>Continuation task.</returns>
        protected abstract Task ExecuteAsync(IProgress<TProgress> progress, CancellationToken cancellationToken);

        private class ProgressManager : IProgress<TProgress>
        {
            private readonly AsyncProgressCommand<TProgress> command;

            public ProgressManager(AsyncProgressCommand<TProgress> command)
            {
                Ensure.NotNull(command, "command");
                this.command = command;
            }

            public void Report(TProgress value)
                => command.Progress = value;
        }
    }

    /// <summary>
    /// A command reporting progress of type <typeparamref name="TProgress"/>.
    /// </summary>
    /// <typeparam name="TProgress">A type of the progress.</typeparam>
    public abstract class AsyncProgressCommand<TParameter, TProgress> : AsyncCommand<TParameter>
    {
        private TProgress progress;

        /// <summary>
        /// Gets or sets current progress.
        /// </summary>
        public TProgress Progress
        {
            get { return progress; }
            protected set
            {
                if (!Equals(progress, value))
                {
                    progress = value;
                    RaisePropertyChanged();
                }
            }
        }

        protected override Task ExecuteAsync(TParameter parameter, CancellationToken cancellationToken)
        {
            var progress = new ProgressManager(this);
            return ExecuteAsync(parameter, progress, cancellationToken);
        }

        /// <summary>
        /// Executes asynchronous operation.
        /// </summary>
        /// <param name="progress">A progress reporter.</param>
        /// <param name="cancellationToken">A token to indicate a cancellation request.</param>
        /// <returns>Continuation task.</returns>
        protected abstract Task ExecuteAsync(TParameter parameter, IProgress<TProgress> progress, CancellationToken cancellationToken);

        private class ProgressManager : IProgress<TProgress>
        {
            private readonly AsyncProgressCommand<TParameter, TProgress> command;

            public ProgressManager(AsyncProgressCommand<TParameter, TProgress> command)
            {
                Ensure.NotNull(command, "command");
                this.command = command;
            }

            public void Report(TProgress value)
                => command.Progress = value;
        }
    }
}
