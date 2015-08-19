using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap.Processing.Metadata.Sorting
{
    /// <summary>
    /// Longest constructor based implementation of <see cref="ISortInputProvider"/>.
    /// </summary>
    public class ConstructorSortInputProvider : ISortInputProvider
    {
        public IEnumerable<Type> GetInputs(Type type)
        {
            ConstructorInfo result = null;
            int resultParameterCount = -1;
            foreach (ConstructorInfo constructor in type.GetConstructors())
            {
                int parameterCount = constructor.GetParameters().Length;
                if(result == null || resultParameterCount < parameterCount)
                {
                    result = constructor;
                    resultParameterCount = parameterCount;
                }
            }

            if (result == null)
                return Enumerable.Empty<Type>();

            return result.GetParameters().Select(p => p.ParameterType);
        }
    }
}
