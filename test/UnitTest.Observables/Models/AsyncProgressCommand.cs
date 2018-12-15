using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptuo.Observables.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Neptuo.Observables.Models
{
    public class AsyncProgressCommand : AsyncProgressCommand<int>
    {
        protected override bool CanExecuteOverride() => true;

        protected override Task ExecuteAsync(IProgress<int> progress, CancellationToken cancellationToken)
        {
            Assert.IsNotNull(progress);
            progress.Report(1);
            progress.Report(2);
            progress.Report(3);
            return Task.CompletedTask;
        }
    }

    public class AsyncProgressCommandWithParameter : AsyncProgressCommand<DateTime, int>
    {
        protected override bool CanExecuteOverride(DateTime dateTime) => true;

        protected override Task ExecuteAsync(DateTime parameter, IProgress<int> progress, CancellationToken cancellationToken)
        {
            Assert.IsNotNull(progress);
            progress.Report(1);
            progress.Report(2);
            progress.Report(3);
            return Task.CompletedTask;
        }
    }
}
