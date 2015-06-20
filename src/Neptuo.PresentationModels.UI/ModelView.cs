using Neptuo.ComponentModel;
using Neptuo.PresentationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.UI
{
    /// <summary>
    /// Základ serverového pohledu na model.
    /// </summary>
    public abstract class ModelView<T> : DisposableBase, IModelView<T>
    {
        private readonly IModelDefinition modelDefinition;
        private readonly Dictionary<string, IFieldView<T>> fieldViews;
        private bool isRendered;

        /// <summary>
        /// Vytvoří instanci a nastaví definici modelu, pro kterou je tento pohled určen.
        /// </summary>
        /// <param name="modelDefinition">Definici modelu pro tento pohled.</param>
        public ModelView(IModelDefinition modelDefinition)
        {
            Ensure.NotNull(modelDefinition, "modelDefinition");
            this.modelDefinition = modelDefinition;
            this.fieldViews = new Dictionary<string, IFieldView<T>>();
        }

        /// <summary>
        /// Zaregistruje podle pro field, takže z něj bude možné získat hodnotu.
        /// </summary>
        /// <param name="fieldIdentifier">Identifikátor fieldu.</param>
        /// <param name="fieldView">Pohled pro daný field.</param>
        protected void AddFieldView(string fieldIdentifier, IFieldView<T> fieldView)
        {
            Ensure.NotNullOrEmpty(fieldIdentifier, "fieldIdentifier");
            Ensure.NotNull(fieldView, "fieldView");
            fieldViews[fieldIdentifier] = fieldView;
        }

        #region IModelView

        public bool TryGetValue(string identifier, out object value)
        {
            if (isRendered)
            {
                IFieldView<T> fieldView;
                if (fieldViews.TryGetValue(identifier, out fieldView))
                    return fieldView.TryGetValue(out value);
            }

            value = null;
            return false;
        }

        public bool TrySetValue(string identifier, object value)
        {
            if (isRendered)
            {
                IFieldView<T> fieldView;
                if (fieldViews.TryGetValue(identifier, out fieldView))
                    return fieldView.TrySetValue(value);
            }

            return false;
        }

        public void Render(T parent)
        {
            if (!isRendered)
            {
                isRendered = true;
                RenderInternal(parent);
            }
        }

        protected abstract void RenderInternal(T parent);

        #endregion
    }
}
