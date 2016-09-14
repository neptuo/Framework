using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpKit.JavaScript.Ast;

namespace SharpKit.UnobtrusiveFeatures.Expressions
{
    public class ExpressionVisitor : JsNodeVisitor
    {
        private ExpressionCache cache;
        private Log log;

        public ExpressionVisitor(ExpressionCache cache, Log log)
        {
            this.cache = cache;
            this.log = log;
        }

        protected override void Visit(JsInvocationExpression node)
        {
            JsMemberExpression rootMember = node.Member as JsMemberExpression;
            if (rootMember != null)
            {
                IEnumerable<ExpressionCacheItem> cacheItems = cache.GetByMethodName(rootMember.Name);
                foreach (ExpressionCacheItem cacheItem in cacheItems)
                {
                    JsInvocationExpression argumentExpression = node.Arguments[cacheItem.ParameterIndex] as JsInvocationExpression;
                    if (argumentExpression != null)
                    {
                        if (argumentExpression.Arguments.Count > 1)
                        {
                            JsFunction function = argumentExpression.Arguments[1] as JsFunction;
                            if (function != null)
                            {
                                if (function.Block.Statements.Count > 0)
                                {
                                    JsReturnStatement returnStatement = function.Block.Statements[0] as JsReturnStatement;
                                    if (returnStatement != null)
                                    {
                                        Stack<string> names = new Stack<string>();
                                        JsMemberExpression member = (JsMemberExpression)((JsInvocationExpression)returnStatement.Expression).Member;
                                        while (member != null)
                                        {
                                            names.Push(member.Name);
                                            if (member.PreviousMember is JsMemberExpression)
                                                member = (JsMemberExpression)member.PreviousMember;
                                            else if (member.PreviousMember is JsInvocationExpression)
                                                member = ((JsInvocationExpression)member.PreviousMember).Member as JsMemberExpression;
                                            else
                                                member = null;
                                        }
                                        names.Pop();

                                        string expressionContent = String.Join(".", ProcessExpressionParts(names));
                                        node.Arguments[cacheItem.ParameterIndex] = new JsCodeExpression { Code = "\"" + expressionContent + "\"" };
                                        log.Debug("Replacing expression in {0}, for '{1}'", cacheItem.Method.Name, expressionContent);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            base.Visit(node);
        }

        protected const string getterStart = "get_";

        protected IEnumerable<string> ProcessExpressionParts(IEnumerable<string> parts)
        {
            foreach (string part in parts)
            {
                if (part.StartsWith(getterStart))
                    yield return part.Substring(getterStart.Length);
                else
                    yield return part;
            }
        }
    }
}
 