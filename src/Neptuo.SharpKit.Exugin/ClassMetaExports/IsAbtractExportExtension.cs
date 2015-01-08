using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSharpCode.NRefactory.TypeSystem;
using SharpKit.JavaScript.Ast;

namespace Neptuo.SharpKit.Exugin.ClassMetaExports
{
    /// <summary>
    /// Exportuje informaci o tom, zda je třída abstraktní.
    /// </summary>
    public class IsAbtractExportExtension : BaseTypeProcessExtension
    {
        public IsAbtractExportExtension(bool debug)
            : base("IsAbtractExport", debug)
        { }

        protected override void Process(ITypeDefinition type, JsUnit unit)
        {
            List<JsJsonNameValue> target = Helper.GetClassContent(unit);
            JsJsonNameValue abstractValue = new JsJsonNameValue { Name = Helper.JsJsonMember("IsAbstract"), Value = Helper.JsValue(type.IsAbstract) };
            target.Add(abstractValue);
        }
    }
}
