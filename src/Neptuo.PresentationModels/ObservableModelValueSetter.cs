using Neptuo.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels
{
    /// <summary>
    /// Implementation of <see cref="INotifyPropertyChanged"/> and <see cref="IModelValueSetter"/>.
    /// Every successfull set to inner setter raises <see cref="INotifyPropertyChanged.PropertyChanged"/>.
    /// </summary>
    public class ObservableModelValueSetter : DisposableBase, IModelValueSetter, INotifyPropertyChanged
    {
        private readonly IModelValueSetter innerSetter;

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Creates new instance that wraps <paramref name="innerSetter"/> and raises <see cref="INotifyPropertyChanged.PropertyChanged"/>.
        /// </summary>
        /// <param name="innerSetter">Inner setter.</param>
        public ObservableModelValueSetter(IModelValueSetter innerSetter)
        {
            Ensure.NotNull(innerSetter, "innerSetter");
            this.innerSetter = innerSetter;
        }

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            Ensure.NotNull(propertyName, "propertyName");
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual bool TrySetValue(string identifier, object value)
        {
            if (innerSetter.TrySetValue(identifier, value))
            {
                RaisePropertyChanged(identifier);
                return true;
            }

            return false;
        }
    }
}
