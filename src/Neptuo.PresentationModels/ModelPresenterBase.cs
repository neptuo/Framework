using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels
{
    public abstract class ModelPresenterBase : IModelPresenter
    {
        protected bool IsPreparedCalled { get; private set; }
        protected IModelDefinition ModelDefinition { get; set; }
        protected IModelView ModelView { get; set; }
        protected Dictionary<string, IFieldDefinition> FieldsByIdentifier { get; private set; }

        public ModelPresenterBase()
        {
            FieldsByIdentifier = new Dictionary<string, IFieldDefinition>();
        }

        public virtual IModelPresenter SetModel(IModelDefinition modelDefinition)
        {
            if (IsPreparedCalled)
                throw new InvalidOperationException("Unable to set model definition after calling Prepare.");

            ModelDefinition = modelDefinition;
            return this;
        }

        public virtual IModelPresenter SetView(IModelView modelView)
        {
            if (IsPreparedCalled)
                throw new InvalidOperationException("Unable to set model view after calling Prepare.");

            ModelView = modelView;
            return this;
        }

        public virtual void Prepare()
        {
            if (IsPreparedCalled)
                throw new InvalidOperationException("Unable to call Prepare multiple times.");

            foreach (IFieldDefinition field in ModelDefinition.Fields)
                FieldsByIdentifier[field.Identifier] = field;

            IsPreparedCalled = true;
        }

        public virtual void SetData(IModelValueGetter getter)
        {
            object value;
            foreach (IFieldDefinition field in ModelDefinition.Fields)
            {
                if (getter.TryGetValue(field.Identifier, out value))
                    ModelView.SetValue(field.Identifier, value);
            }
        }

        public virtual void GetData(IModelValueSetter setter)
        {
            foreach (IFieldDefinition field in ModelDefinition.Fields)
                setter.SetValue(field.Identifier, ModelView.GetValue(field.Identifier));
        }
    }
}
