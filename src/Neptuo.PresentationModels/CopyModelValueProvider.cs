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
                        targetSetter.SetValue(field.Identifier, value);
                }
            }
        }
    }
}
