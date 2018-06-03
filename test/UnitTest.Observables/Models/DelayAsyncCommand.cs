using Neptuo.Observables.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Neptuo.Observables.Models
{
    public class DelayAsyncCommand : AsyncCommand
    {
        public bool IsExecutable { get; set; } = true;

        public bool IsPhase1Reached { get; set; }
        public bool IsPhase2Reached { get; set; }

        protected override bool CanExecuteOverride()
        {
            return IsExecutable;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            IsPhase1Reached = true;
            await Task.Delay(500);

            cancellationToken.ThrowIfCancellationRequested();

            IsPhase2Reached = true;
            await Task.Delay(500);
        }
    }
}
