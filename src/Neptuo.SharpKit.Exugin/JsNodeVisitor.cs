using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSharpCode.NRefactory.TypeSystem.Implementation;
using SharpKit.JavaScript.Ast;

namespace SharpKit.UnobtrusiveFeatures
{
    public class JsNodeVisitor : JsNodeVisitorBase
    {
        protected override void Visit(JsAssignmentExpression node)
        {
            if (node != null)
            {
                Visit(node.Left);
                Visit(node.Right);
            }
        }

        protected override void Visit(JsBinaryExpression node)
        {
            if (node != null)
            {
                Visit(node.Left);
                Visit(node.Right);
            }
        }

        protected override void Visit(JsBlock node)
        {
            if (node != null)
            {
                foreach (JsStatement statement in node.Statements)
                {
                    Visit(statement);
                }
            }
        }

        protected override void Visit(JsBreakStatement node)
        {
            // Do Nothing
        }

        protected override void Visit(JsCatchClause node)
        {
            if (node != null)
            {
                // TODO: node.IdentifierName ?
                Visit(node.Block);
            }
        }

        protected override void Visit(JsCodeExpression node)
        {
            if (node != null)
            {
                // TODO: JsCodeExpression
            }
        }

        protected override void Visit(JsCommentStatement node)
        {
            if (node != null)
            {
                // Ignore comments
            }
        }

        protected override void Visit(JsConditionalExpression node)
        {
            if (node != null)
            {
                Visit(node.Condition);
                Visit(node.TrueExpression);
                Visit(node.FalseExpression);
            }
        }

        protected override void Visit(JsContinueStatement node)
        {
            if (node != null)
            {
                // Do Nothing
            }
        }

        protected override void Visit(JsDoWhileStatement node)
        {
            if (node != null)
            {
                Visit(node.Condition);
                Visit(node.Statement);
            }
        }

        protected override void Visit(JsExpression node)
        {
            if (node != null)
            {
                // TODO: JsExpression
                if (node is JsInvocationExpression)
                    Visit((JsInvocationExpression)node);
                else if (node is JsFunction)
                    Visit((JsFunction)node);
                else if (node is JsMemberExpression)
                    Visit((JsMemberExpression)node);
                
            }
        }

        protected override void Visit(JsExpressionStatement node)
        {
            if (node != null)
            {
                Visit(node.Expression);
            }
        }

        protected override void Visit(JsExternalFileUnit node)
        {
            if (node != null)
            {
                // Do Nothing
            }
        }

        protected override void Visit(JsForInStatement node)
        {
            if (node != null)
            {
                Visit(node.Initializer);
                Visit(node.Member);
                Visit(node.Statement);
            }
        }

        protected override void Visit(JsForStatement node)
        {
            if (node != null)
            {
                Visit(node.Condition);

                foreach (JsStatement item in node.Initializers)
                    Visit(item);

                foreach (JsStatement item in node.Iterators)
                    Visit(item);

                Visit(node.Statement);
            }
        }

        protected override void Visit(JsFunction node)
        {
            if (node != null)
            {
                Visit(node.Block);
            }
        }

        protected override void Visit(JsIfStatement node)
        {
            if (node != null)
            {
                Visit(node.Condition);
                Visit(node.ElseStatement);
                Visit(node.IfStatement);
            }
        }

        protected override void Visit(JsIndexerAccessExpression node)
        {
            if (node != null)
            {
                Visit(node.Member);

                foreach (JsExpression arg in node.Arguments)
                {
                    Visit(arg);
                }
            }
        }

        protected override void Visit(JsInvocationExpression node)
        {
            if (node != null)
            {
                Visit(node.Member);
                foreach (JsExpression arg in node.Arguments)
                {
                    Visit(arg);
                }
            }
        }

        protected override void Visit(JsJsonArrayExpression node)
        {
            if (node != null)
            {
                foreach (JsExpression esp in node.Items)
                {
                    Visit(esp);
                }
            }
        }

        protected override void Visit(JsJsonMember node)
        {
            if (node != null)
            {
                // TODO: JsJsonMember -- node.Name?
            }
        }

        protected override void Visit(JsJsonNameValue node)
        {
            if (node != null)
            {
                Visit(node.Name);
                Visit(node.Value);
            }
        }

        protected override void Visit(JsJsonObjectExpression node)
        {
            if (node != null && node.NamesValues != null)
            {
                foreach (JsJsonNameValue nameValue in node.NamesValues)
                {
                    Visit(nameValue);
                }
            }
        }

        protected override void Visit(JsMemberExpression node)
        {
            if (node != null)
            {
                Visit(node.PreviousMember);

                // bschmidtke : We may need this although I don't think so. Keeping around for node.Name, just in case.

                //string jsStr = node.ToJs();
                //string regStr = @"(?:Typeof\(|new\s)([\w\.]+)(?:\(|\))";
                //MatchCollection match = Regex.Matches( node.Name, regStr, RegexOptions.None );
                //if ( match.Count > 0 )
                //{
                //    Visit(node, true );
                //}
                //else
                //{
                //    // end of the line for this guy
                //}
            }
        }

        // new object expression adds the "new" work prior to defining an object.
        protected override void Visit(JsNewObjectExpression node)
        {
            if (node != null)
            {
                Visit(node.Invocation);
            }
        }

        protected override void Visit(JsNodeList node)
        {
            if (node != null)
            {
                foreach (JsNode jsnode in node.Nodes)
                {
                    Visit(jsnode);
                }
            }
        }

        protected override void Visit(JsNullExpression node)
        {
            if (node != null)
            {
                // Do Nothing
            }
        }

        protected override void Visit(JsNumberExpression node)
        {
            if (node != null)
            {
                // Do Nothing
            }
        }

        protected override void Visit(JsParenthesizedExpression node)
        {
            if (node != null)
            {
                Visit(node.Expression);
            }
        }

        protected override void Visit(JsPostUnaryExpression node)
        {
            if (node != null)
            {
                Visit(node.Left);
            }
        }

        protected override void Visit(JsPreUnaryExpression node)
        {
            if (node != null)
            {
                Visit(node.Right);
            }
        }

        protected override void Visit(JsRegexExpression node)
        {
            if (node != null)
            {
                // Do Nothing
            }
        }

        protected override void Visit(JsReturnStatement node)
        {
            if (node != null)
            {
                Visit(node.Expression);
            }
        }

        protected override void Visit(JsStatement node)
        {
            if (node != null)
            {
                // TODO: JsStatement?
                if (node is JsExpressionStatement)
                    Visit((JsExpressionStatement)node);
                else if (node is JsIfStatement)
                    Visit((JsIfStatement)node);
                else if (node is JsBlock)
                    Visit((JsBlock)node);
                else if (node is JsTryStatement)
                    Visit((JsTryStatement)node);
            }
        }

        protected override void Visit(JsStatementExpressionList node)
        {
            if (node != null)
            {
                foreach (JsExpression expression in node.Expressions)
                {
                    Visit(expression);
                }
            }
        }

        protected override void Visit(JsStringExpression node)
        {
            if (node != null)
            {
                // TODO: JsStringExpression?
            }
        }

        protected override void Visit(JsSwitchLabel node)
        {
            if (node != null)
            {
                Visit(node.Expression);
            }
        }

        protected override void Visit(JsSwitchSection node)
        {
            if (node != null)
            {
                foreach (JsSwitchLabel label in node.Labels)
                {
                    Visit(label);
                }

                foreach (JsStatement statement in node.Statements)
                {
                    Visit(statement);
                }
            }
        }

        protected override void Visit(JsSwitchStatement node)
        {
            if (node != null)
            {
                Visit(node.Expression);
                foreach (JsSwitchSection section in node.Sections)
                {
                    Visit(section);
                }
            }
        }

        protected override void Visit(JsThis node)
        {
            if (node != null)
            {
                // TODO: JsThis?
            }
        }

        protected override void Visit(JsThrowStatement node)
        {
            if (node != null)
            {
                Visit(node.Expression);
            }
        }

        protected override void Visit(JsTryStatement node)
        {
            if (node != null)
            {
                Visit(node.TryBlock);
                Visit(node.CatchClause);
                Visit(node.FinallyBlock);
            }
        }

        protected override void Visit(JsUnit node)
        {
            if (node != null)
            {
                foreach (JsStatement statement in node.Statements)
                {
                    Visit(statement);
                }
            }
        }

        protected override void Visit(JsVariableDeclarationExpression node)
        {
            if (node != null)
            {
                foreach (JsVariableDeclarator decNode in node.Declarators)
                {
                    Visit(decNode);
                }
            }
        }

        protected override void Visit(JsVariableDeclarationStatement node)
        {
            if (node != null)
            {
                Visit(node.Declaration);
            }
        }

        protected override void Visit(JsVariableDeclarator node)
        {
            if (node != null)
            {
                Visit(node.Initializer);
            }
        }


        protected override void Visit(JsWhileStatement node)
        {
            if (node != null)
            {
                Visit(node.Condition);
                Visit(node.Statement);
            }
        }

        protected override void Visit(JsUseStrictStatement node)
        {
            if (node != null)
            {
                // Do Nothing
            }
        }
    }
}
