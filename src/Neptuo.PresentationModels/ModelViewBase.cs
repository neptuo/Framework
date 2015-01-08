using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels
{
    public abstract class ModelViewBase : IModelView
    {
        protected abstract IFieldView GetFieldViewByIdentifier(string identifier);

        public void SetValue(string identifier, object value)
        {
            GetFieldViewByIdentifier(identifier).SetValue(value);
        }

        public object GetValue(string identifier)
        {
            IFieldView fieldView = GetFieldViewByIdentifier(identifier);
            if(fieldView != null)
                return fieldView.GetValue();

            return null;
        }
    }
}
