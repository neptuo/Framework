using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Observables.Commands
{
    /// <summary>
    /// An extension of a <see cref="Command"/> which implementes a <see cref="INotifyPropertyChanged"/> to support publishing notifications.
    /// </summary>
    public abstract class ObservableCommand : Command, INotifyPropertyChanged
    {
        /// <summary>
        /// An event raised when property was changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Runs <see cref="PropertyChanged"/> caused by property <paramref name="propertyName"/>.
        /// </summary>
        /// <param name="propertyName">A name of the changed property.</param>
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            Ensure.NotNull(propertyName, "propertyName");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    /// <summary>
    /// An extension of a <see cref="Command"/> which implementes a <see cref="INotifyPropertyChanged"/> to support publishing notifications.
    /// </summary>
    public abstract class ObservableCommand<T> : Command<T>, INotifyPropertyChanged
    {
        /// <summary>
        /// An event raised when property was changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Runs <see cref="PropertyChanged"/> caused by property <paramref name="propertyName"/>.
        /// </summary>
        /// <param name="propertyName">A name of the changed property.</param>
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            Ensure.NotNull(propertyName, "propertyName");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
