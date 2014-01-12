using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSharpCode.NRefactory.TypeSystem;
using SharpKit.JavaScript.Ast;

namespace Neptuo.SharpKit.Exugin.Expressions
{
    public class ExpressionExtension : BaseExtension
    {
        private ExpressionCache cache;
        private ExpressionVisitor visitor;

        public ExpressionExtension(bool debug)
            : base("Expression", debug)
        {
            cache = new ExpressionCache();
            visitor = new ExpressionVisitor(cache, Log);
        }

        public void PrepareMethodCache(IEnumerable<IAssembly> assemblies)
        {
            foreach (IAssembly assembly in assemblies)
            {
                foreach (ITypeDefinition type in assembly.GetAllTypeDefinitions())
                {
                    foreach (IMethod method in type.GetMethods())
                    {
                        int i = 0;
                        foreach (IParameter parameter in method.Parameters)
                        {
                            if (type.FullName.StartsWith("Magic") && parameter.Type.FullName.StartsWith("System.Linq.Expressions.Expression"))
                                cache.Add(new ExpressionCacheItem(type, method, parameter, i + method.TypeArguments.Count));

                            i++;
                        }
                    }
                }
            }
        }

        public void Process(IEntity entity, JsNode node)
        {
            //if (entity.Name == "SuperTest")
            //{
                //JsExpressionStatement expression = (JsExpressionStatement)((JsFunction)((JsJsonNameValue)node).Value).Block.Statements[0];

                //JsReturnStatement returnStatement = ((JsReturnStatement)((JsFunction)((JsInvocationExpression)((JsInvocationExpression)expression.Expression).Arguments[0]).Arguments[1]).Block.Statements[0]);
                //string expressionContent = ((JsMemberExpression)((JsInvocationExpression)returnStatement.Expression).Member).Name;

                //((JsInvocationExpression)expression.Expression).Arguments[0] = new JsCodeExpression { Code = "\"" + expressionContent + "\"" };

                //JsBlock block = ((JsFunction)((JsJsonNameValue)node).Value).Block;
                visitor.Visit(node);

                //foreach (JsStatement statement in block.Statements)
                //{
                //    JsExpressionStatement expression = statement as JsExpressionStatement;
                //    if (expression != null)
                //    {
                //        JsInvocationExpression invocation = expression.Expression as JsInvocationExpression;
                //        if (invocation != null)
                //        {
                //            ExpressionVisitor visitor = new ExpressionVisitor(cache);
                //            visitor.Visit(invocation);
                //        }
                //    }
                //}
            //}
            
        }
    }
}
