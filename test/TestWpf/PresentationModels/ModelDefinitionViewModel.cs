using Neptuo;
using Neptuo.ComponentModel;
using Neptuo.Observables;
using Neptuo.Observables.Collections;
using Neptuo.PresentationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWpf.PresentationModels
{
    public class ModelDefinitionViewModel : ObservableObject//, IModelView
    {
        public IModelDefinition ModelDefinition { get; private set; }
        public ObservableCollection<FieldDefinitionViewModel> Fields { get; private set; }

        public ModelDefinitionViewModel(IModelDefinition modelDefinition)
        {
            Ensure.NotNull(modelDefinition, "modelDefinition");
            ModelDefinition = modelDefinition;
            Fields = new ObservableCollection<FieldDefinitionViewModel>(modelDefinition.Fields.Select(f => new FieldDefinitionViewModel(f)));
        }

        public void SetValue(string identifier, object value)
        {
            foreach (FieldDefinitionViewModel field in Fields)
            {
                if(field.FieldDefinition.Identifier == identifier)
                {
                    field.SetValue(value);
                    break;
                }
            }
        }

        public object GetValue(string identifier)
        {
            foreach (FieldDefinitionViewModel field in Fields)
            {
                if (field.FieldDefinition.Identifier == identifier)
                    return field.GetValue();
            }

            throw new InvalidOperationException(String.Format("Model definition '{0}' doesn't contain field with identifier '{1}'.", ModelDefinition.Identifier, identifier));
        }
    }
}
