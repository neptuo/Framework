using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Neptuo.Windows.Controls
{
    public class FocusCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, EventArgs.Empty);

            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            IInputElement target = parameter as IInputElement;
            if (target != null)
                target.Focus();
        }
    }
}
