using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap.Hierarchies.Sorting
{
    /// <summary>
    /// Provides sorting alogorithm.
    /// For every input, output provider is executed first.
    /// </summary>
    internal class Sorter
    {
        private readonly ISortInputProvider inputProvider;
        private readonly ISortOutputProvider outputProvider;

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="inputProvider">Input parameter provider.</param>
        /// <param name="outputProvider">Output parameter provider.</param>
        public Sorter(ISortInputProvider inputProvider, ISortOutputProvider outputProvider)
        {
            this.inputProvider = inputProvider;
            this.outputProvider = outputProvider;
        }

        /// <summary>
        /// Sorts <paramref name="types"/> so "every input, output provider is executed first".
        /// </summary>
        /// <param name="types">Types to sort.</param>
        /// <param name="defaultOutputs">Default (already available) inputs.</param>
        /// <returns>Sorted <paramref name="types"/>.</returns>
        public IEnumerable<Type> Sort(IEnumerable<Type> types, List<Type> defaultOutputs)
        {
            Ensure.NotNull(types, "types");
            Ensure.NotNull(defaultOutputs, "defaultOutputs");

            Dictionary<Type, List<Type>> inputs = new Dictionary<Type, List<Type>>();
            Dictionary<Type, Type> outputs = new Dictionary<Type,Type>();

            foreach (Type type in types)
            {
                inputs[type] = new List<Type>(inputProvider.GetInputs(type));
                
                foreach (Type outputType in outputProvider.GetOutputs(type))
                    outputs[outputType] = type;
            }

            List<Type> result = new List<Type>();
            Stack<Type> current = new Stack<Type>();

            foreach (Type type in types)
                InsertType(result, type, inputs, outputs, defaultOutputs, current);

            return result;
        }

        private void InsertType(List<Type> result, Type type, Dictionary<Type, List<Type>> inputs, Dictionary<Type, Type> outputs, List<Type> defaultOutputs, Stack<Type> current)
        {
            if (result.Contains(type))
                return;

            if (current.Contains(type))
                throw Ensure.Exception.InvalidOperation("Unnable to sort task '{0}' because cyclic dependency will be created.", type.FullName);

            current.Push(type);

            foreach (Type inputType in inputs[type])
            {
                if (defaultOutputs.Contains(inputType))
                    continue;

                Type providerType;
                if (outputs.TryGetValue(inputType, out providerType))
                    InsertType(result, providerType, inputs, outputs, defaultOutputs, current);
                else
                    throw Ensure.Exception.InvalidOperation("Missing provider for dependency of type '{0}'.", inputType.FullName);
            }

            result.Add(type);
            current.Pop();
        }
    }
}
