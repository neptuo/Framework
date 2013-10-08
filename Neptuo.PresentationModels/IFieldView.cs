using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels
{
    public interface IFieldView
    {
        void SetValue(object value);
        object GetValue();
    }
}
