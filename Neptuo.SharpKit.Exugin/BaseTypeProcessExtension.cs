using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSharpCode.NRefactory.TypeSystem;
using SharpKit.JavaScript.Ast;

namespace Neptuo.SharpKit.Exugin
{
    /// <summary>
    /// Bázový procesor exportovaných typů.
    /// </summary>
    public abstract class BaseTypeProcessExtension : BaseAstExtension
    {
        public BaseTypeProcessExtension(string extensionName, bool debug)
            : base(extensionName, debug)
        { }

        public void Process(IEntity entity, JsNode node)
        {
            if (entity.EntityType == EntityType.TypeDefinition)
                Process((ITypeDefinition)entity, (JsUnit)node);
        }

        protected abstract void Process(ITypeDefinition type, JsUnit unit);
    }
}
