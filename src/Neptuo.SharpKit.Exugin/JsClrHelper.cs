using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpKit.JavaScript.Ast;

namespace SharpKit.UnobtrusiveFeatures
{
    /// <summary>
    /// Helper pro práci s Sharpkitím astem.
    /// </summary>
    public class JsClrHelper
    {
        /// <summary>
        /// Vrací js expression pro bool hodnotu.
        /// </summary>
        /// <param name="value">Hodnota.</param>
        /// <returns>Js expression pro bool hodnotu.</returns>
        public JsCodeExpression JsValue(bool value)
        {
            return new JsCodeExpression { Code = value ? "true" : "false" };
        }

        /// <summary>
        /// Vytvoří <see cref="JsJsonNameValue"/> z předaného názvu a hodnoty.
        /// </summary>
        /// <param name="name">Jméno vlastnosti.</param>
        /// <param name="value">Hodnota vlastnosti.</param>
        /// <returns><see cref="JsJsonNameValue"/>.</returns>
        public JsJsonNameValue JsJsonNameValue(JsJsonMember name, JsExpression value)
        {
            return new JsJsonNameValue { Name = name, Value = value };
        }

        /// <summary>
        /// Vytvoří <see cref="JsJsonNameValue"/> z předaného názvu (z něj vytvoří <see cref="JsJsonMember"/>) a hodnoty.
        /// </summary>
        /// <param name="name">Jméno vlastnosti.</param>
        /// <param name="value">Hodnota vlastnosti.</param>
        /// <returns><see cref="JsJsonNameValue"/>.</returns>
        public JsJsonNameValue JsJsonNameValue(string name, JsExpression value)
        {
            return new JsJsonNameValue { Name = JsJsonMember(name), Value = value };
        }

        /// <summary>
        /// Vytvoří <see cref="JsJsonMember"/> z předaného názvu.
        /// </summary>
        /// <param name="name">Jméno.</param>
        /// <returns><see cref="JsJsonMember"/>.</returns>
        public JsJsonMember JsJsonMember(string name)
        {
            return new JsJsonMember { Name = name };
        }

        /// <summary>
        /// Vrací kolekci prvků, které jsou v předané <paramref name="unit"/> (reprezentující třídu v JsClr formátu).
        /// Neodpovídá vlastnostem, metodám a pod z původní C# třídy.
        /// Obsahuje fullName, definition, Kind a pod.
        /// </summary>
        /// <param name="unit">Třída v JsClr formátu</param>
        /// <returns>Obsah třídy.</returns>
        public List<JsJsonNameValue> GetClassContent(JsUnit unit)
        {
            if (unit.Statements.Count > 0)
            {
                JsVariableDeclarationStatement jsVariable = unit.Statements[1] as JsVariableDeclarationStatement;
                if (jsVariable != null)
                    return ((JsJsonObjectExpression)jsVariable.Declaration.Declarators[0].Initializer).NamesValues;
            }
            return new List<JsJsonNameValue>();
        }
    }
}
