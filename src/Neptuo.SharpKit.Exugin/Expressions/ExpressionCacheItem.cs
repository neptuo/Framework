using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSharpCode.NRefactory.TypeSystem;

namespace SharpKit.UnobtrusiveFeatures.Expressions
{
    /// <summary>
    /// Položka cache.
    /// </summary>
    public class ExpressionCacheItem
    {
        public ITypeDefinition Type { get; private set; }
        public IMethod Method { get; private set; }
        public IParameter Parameter { get; private set; }
        public int ParameterIndex { get; private set; }

        public ExpressionCacheItem(ITypeDefinition type, IMethod method, IParameter parameter, int parameterIndex)
        {
            Type = type;
            Method = method;
            Parameter = parameter;
            ParameterIndex = parameterIndex;
        }
    }
}
