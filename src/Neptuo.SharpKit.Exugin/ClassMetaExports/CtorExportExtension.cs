using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSharpCode.NRefactory.TypeSystem;
using SharpKit.JavaScript.Ast;

namespace SharpKit.UnobtrusiveFeatures.ClassMetaExports
{
    /// <summary>
    /// Exportuje konstruktory do js.
    /// </summary>
    public class CtorExportExtension : BaseTypeProcessExtension
    {
        public CtorExportExtension(bool debug)
            : base("CtorExport", debug)
        { }

        /// <summary>
        /// Pokusí vyexportovat informace o jeho ctoru.
        /// </summary>
        /// <param name="type">Typ.</param>
        /// <param name="unit">K němu odpovídající unit.</param>
        protected override void Process(ITypeDefinition type, JsUnit unit)
        {
            List<JsJsonNameValue> target = Helper.GetClassContent(unit);

            JsJsonArrayExpression ctorValues = new JsJsonArrayExpression { Items = new List<JsExpression>() };
            JsJsonNameValue ctors = new JsJsonNameValue { Name = Helper.JsJsonMember("ctors"), Value = ctorValues };
            target.Add(ctors);

            int ctorCount = type.GetConstructors().Count();
            foreach (IMethod ctor in type.GetConstructors())
            {
                JsJsonObjectExpression ctorExpression = new JsJsonObjectExpression { NamesValues = new List<JsJsonNameValue>() };
                JsStringExpression ctorNameExpression = new JsStringExpression();
                ctorExpression.NamesValues.Add(Helper.JsJsonNameValue("name", ctorNameExpression));
                ctorValues.Items.Add(ctorExpression);

                List<string> paramNames = new List<string>(ctor.Parameters.Count + 1) { "ctor" };
                JsJsonArrayExpression ctorParamaters = new JsJsonArrayExpression { Items = new List<JsExpression>() };
                foreach (IParameter parameter in ctor.Parameters)
                {
                    ctorParamaters.Items.Add(new JsStringExpression { Value = parameter.Type.FullName });
                    paramNames.Add(parameter.Type.Name);
                }
                ctorNameExpression.Value = ctorCount == 1 ? "ctor" : String.Join("$$", paramNames);
                ctorExpression.NamesValues.Add(Helper.JsJsonNameValue("parameters", ctorParamaters));
            }
        }
    }
}
