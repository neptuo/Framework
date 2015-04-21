using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels
{
    /// <summary>
    /// Copies values from source getter to target setter.
    /// Instance per model definition.
    /// </summary>
    public class CopyModelValueProvider
    {
        /// <summary>
        /// Model definition.
        /// </summary>
        public IModelDefinition ModelDefinition { get; private set; }

        /// <summary>
        /// Creates new instance for <paramref name="modelDefinition"/>.
        /// </summary>
        /// <param name="modelDefinition">Model definition.</param>
        public CopyModelValueProvider(IModelDefinition modelDefinition)
        {
            Ensure.NotNull(modelDefinition, "modelDefinition");
            ModelDefinition = modelDefinition;
        }

        /// <summary>
        /// Sets values to <paramref name="targetSetter"/> from <paramref name="sourceGetters"/>.
        /// </summary>
        /// <param name="targetSetter">Target.</param>
        /// <param name="sourceGetters">Sources.</param>
        public void Update(IModelValueSetter targetSetter, params IModelValueGetter[] sourceGetters)
        {
            foreach (IFieldDefinition field in ModelDefinition.Fields)
            {
                object value;
                foreach (IModelValueGetter sourceGetter in sourceGetters)
                {
                    if (sourceGetter.TryGetValue(field.Identifier, out value))
                    {
                        if (!IsAssignable(field.FieldType, value))
                            value = Converts.To(field.FieldType, value);

                        targetSetter.TrySetValue(field.Identifier, value);
                    }
                }
            }
        }

        /// <summary>
        /// Determines whether <paramref name="value"/> can be assigned to field of type <paramref name="fieldType"/>.
        /// </summary>
        /// <param name="fieldType">Required type.</param>
        /// <param name="value">Current value.</param>
        /// <returns><c>true</c> if <paramref name="value"/> can be assigned with <paramref name="value"/>; <c>false</c> otherwise.</returns>
        private bool IsAssignable(Type fieldType, object value)
        {
            if (value != null)
                return fieldType.IsAssignableFrom(value.GetType());

            if (fieldType.IsValueType)
                return false;

            return true;
        }
    }
}
