using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Observables
{
    /// <summary>
    /// A base implementation of <see cref="INotifyPropertyChanged"/>.
    /// </summary>
    public class ObservableModel : INotifyPropertyChanged
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
