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
    /// Implementation of <see cref="INotifyPropertyChanged"/> and <see cref="IModelValueProvider"/>.
    /// Every successfull set to inner setter raises <see cref="INotifyPropertyChanged.PropertyChanged"/>.
    /// If value is not changed, set is not called.
    /// </summary>
    public class ObservableModelValueProvider : ObservableModelValueSetter, IModelValueProvider
    {
        private readonly IModelValueProvider innerProvider;

        /// <summary>
        /// Creates new instance that wraps <paramref name="innerSetter"/> and raises <see cref="INotifyPropertyChanged.PropertyChanged"/>.
        /// </summary>
        /// <param name="innerSetter">Inner provider.</param>
        public ObservableModelValueProvider(IModelValueProvider innerProvider)
            : base(innerProvider)
        {
            Ensure.NotNull(innerProvider, "innerProvider");
            this.innerProvider = innerProvider;
        }

        public bool TryGetValue(string identifier, out object value)
        {
            return innerProvider.TryGetValue(identifier, out value);
        }

        public override bool TrySetValue(string identifier, object value)
        {
            object currentValue;
            if (innerProvider.TryGetValue(identifier, out currentValue) && currentValue == value)
                return true;

            return base.TrySetValue(identifier, value);
        }
    }
}
