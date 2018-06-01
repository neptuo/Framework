using Neptuo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Neptuo.Observables.Commands
{
    /// <summary>
    /// Async version of <see cref="DelegateCommand"/>.
    /// </summary>
    public class AsyncDelegateCommand : AsyncCommand
    {
        private readonly Func<CancellationToken, Task> execute;
        private readonly Func<bool> canExecute;

        /// <summary>
        /// Creates new instance where <paramref name="execute"/> and <paramref name="canExecute"/> can't be <c>null</c>.
        /// </summary>
        /// <param name="execute">A delegate to be executed when the command is executed.</param>
        /// <param name="canExecute">A delegate to be execute when the 'can execute' is executed.</param>
        public AsyncDelegateCommand(Func<Task> execute, Func<bool> canExecute)
            : this(c => execute(), canExecute)
        {
            Ensure.NotNull(execute, "execute");
        }

        /// <summary>
        /// Creates new instance where <paramref name="execute"/> and <paramref name="canExecute"/> can't be <c>null</c>.
        /// </summary>
        /// <param name="execute">A delegate to be executed when the command is executed.</param>
        /// <param name="canExecute">A delegate to be execute when the 'can execute' is executed.</param>
        public AsyncDelegateCommand(Func<CancellationToken, Task> execute, Func<bool> canExecute)
        {
            Ensure.NotNull(execute, "execute");
            Ensure.NotNull(canExecute, "canExecute");
            this.execute = execute;
            this.canExecute = canExecute;
        }

        protected override bool CanExecuteOverride()
             => canExecute();

        protected override Task ExecuteAsync(CancellationToken cancellationToken)
            => execute(cancellationToken);
    }

    /// <summary>
    /// Async version of <see cref="DelegateCommand{T}"/>.
    /// </summary>
    /// <typeparam name="T">A type of the parameter.</typeparam>
    public class AsyncDelegateCommand<T> : AsyncCommand<T>
    {
        private readonly Func<T, CancellationToken, Task> execute;
        private readonly Func<T, bool> canExecute;

        /// <summary>
        /// Creates new instance where <paramref name="execute"/> and <paramref name="canExecute"/> can't be <c>null</c>.
        /// </summary>
        /// <param name="execute">A delegate to be executed when the command is executed.</param>
        /// <param name="canExecute">A delegate to be execute when the 'can execute' is executed.</param>
        public AsyncDelegateCommand(Func<T, Task> execute, Func<T, bool> canExecute)
            : this((p, c) => execute(p), canExecute)
        {
            Ensure.NotNull(execute, "execute");
        }

        /// <summary>
        /// Creates new instance where <paramref name="execute"/> and <paramref name="canExecute"/> can't be <c>null</c>.
        /// </summary>
        /// <param name="execute">A delegate to be executed when the command is executed.</param>
        /// <param name="canExecute">A delegate to be execute when the 'can execute' is executed.</param>
        public AsyncDelegateCommand(Func<T, CancellationToken, Task> execute, Func<T, bool> canExecute)
        {
            Ensure.NotNull(execute, "execute");
            Ensure.NotNull(canExecute, "canExecute");
            this.execute = execute;
            this.canExecute = canExecute;
        }

        protected override bool CanExecuteOverride(T parameter)
             => canExecute(parameter);

        protected override Task ExecuteAsync(T parameter, CancellationToken cancellationToken)
            => execute(parameter, cancellationToken);
    }
}
