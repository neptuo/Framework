using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap.Dependencies.Sorting
{
    public class HierarchySorter
    {
        private readonly ISortInputProvider inputProvider;
        private readonly ISortOutputProvider outputProvider;

        public HierarchySorter(ISortInputProvider inputProvider, ISortOutputProvider outputProvider)
        {
            this.inputProvider = inputProvider;
            this.outputProvider = outputProvider;
        }

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
                throw new Exception(); //TODO: Correct exception for circular reference.

            current.Push(type);

            foreach (Type inputType in inputs[type])
            {
                Type providerType = outputs[inputType]; //TODO: Can cause exception.
                InsertType(result, providerType, inputs, outputs, defaultOutputs, current);
            }

            result.Add(type);
            current.Pop();
        }
    }
}
