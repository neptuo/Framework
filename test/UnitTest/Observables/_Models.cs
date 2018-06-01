using Neptuo.Linq.Expressions;
using Neptuo.Observables.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Neptuo.Observables
{
    public class MainModel : ObservableModel
    {
        private static readonly string fullNamePropertyName = TypeHelper.PropertyName<MainModel>(m => m.FullName);

        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set
            {
                if (firstName != value)
                {
                    firstName = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged(fullNamePropertyName);
                }
            }
        }

        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set
            {
                if (lastName != value)
                {
                    lastName = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged(fullNamePropertyName);
                }
            }
        }

        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }
    }

    public class TestAsyncCommand : AsyncCommand
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
