using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSharpCode.NRefactory.TypeSystem.Implementation;
using SharpKit.JavaScript.Ast;

namespace SharpKit.UnobtrusiveFeatures
{
    public class JsNodeVisitorBase
    {
        public void Visit(JsNode node)
        {
            if (node != null)
            {
                switch (node.NodeType)
                {
                    case JsNodeType.AssignmentExpression:
                        Visit((JsAssignmentExpression)node);
                        break;
                    case JsNodeType.BinaryExpression:
                        Visit((JsBinaryExpression)node);
                        break;
                    case JsNodeType.Block:
                        Visit((JsBlock)node);
                        break;
                    case JsNodeType.BreakStatement:
                        Visit((JsBreakStatement)node);
                        break;
                    case JsNodeType.CatchClause:
                        Visit((JsCatchClause)node);
                        break;
                    case JsNodeType.CodeExpression:
                        Visit((JsCodeExpression)node);
                        break;
                    case JsNodeType.CommentStatement:
                        Visit((JsCommentStatement)node);
                        break;
                    case JsNodeType.ConditionalExpression:
                        Visit((JsConditionalExpression)node);
                        break;
                    case JsNodeType.ContinueStatement:
                        Visit((JsContinueStatement)node);
                        break;
                    case JsNodeType.DoWhileStatement:
                        Visit((JsDoWhileStatement)node);
                        break;
                    case JsNodeType.Expression:
                        Visit((JsExpression)node);
                        break;
                    case JsNodeType.ExpressionStatement:
                        Visit((JsExpressionStatement)node);
                        break;
                    case JsNodeType.ExternalFileUnit:
                        Visit((JsExternalFileUnit)node);
                        break;
                    case JsNodeType.ForInStatement:
                        Visit((JsForInStatement)node);
                        break;
                    case JsNodeType.ForStatement:
                        Visit((JsForStatement)node);
                        break;
                    case JsNodeType.Function:
                        Visit((JsFunction)node);
                        break;
                    case JsNodeType.IfStatement:
                        Visit((JsIfStatement)node);
                        break;
                    case JsNodeType.IndexerAccessExpression:
                        Visit((JsIndexerAccessExpression)node);
                        break;
                    case JsNodeType.InvocationExpression:
                        Visit((JsInvocationExpression)node);
                        break;
                    case JsNodeType.JsonArrayExpression:
                        Visit((JsJsonArrayExpression)node);
                        break;
                    case JsNodeType.JsonMember:
                        Visit((JsJsonMember)node);
                        break;
                    case JsNodeType.JsonNameValue:
                        Visit((JsJsonNameValue)node);
                        break;
                    case JsNodeType.JsonObjectExpression:
                        Visit((JsJsonObjectExpression)node);
                        break;
                    case JsNodeType.MemberExpression:
                        Visit((JsMemberExpression)node);
                        break;
                    case JsNodeType.NewObjectExpression:
                        Visit((JsNewObjectExpression)node);
                        break;
                    case JsNodeType.NodeList:
                        Visit((JsNodeList)node);
                        break;
                    case JsNodeType.NullExpression:
                        Visit((JsNullExpression)node);
                        break;
                    case JsNodeType.NumberExpression:
                        Visit((JsNumberExpression)node);
                        break;
                    case JsNodeType.ParenthesizedExpression:
                        Visit((JsParenthesizedExpression)node);
                        break;
                    case JsNodeType.PostUnaryExpression:
                        Visit((JsPostUnaryExpression)node);
                        break;
                    case JsNodeType.PreUnaryExpression:
                        Visit((JsPreUnaryExpression)node);
                        break;
                    case JsNodeType.RegexExpression:
                        Visit((JsRegexExpression)node);
                        break;
                    case JsNodeType.ReturnStatement:
                        Visit((JsReturnStatement)node);
                        break;
                    case JsNodeType.Statement:
                        Visit((JsStatement)node);
                        break;
                    case JsNodeType.StatementExpressionList:
                        Visit((JsStatementExpressionList)node);
                        break;
                    case JsNodeType.StringExpression:
                        Visit((JsStringExpression)node);
                        break;
                    case JsNodeType.SwitchLabel:
                        Visit((JsSwitchLabel)node);
                        break;
                    case JsNodeType.SwitchSection:
                        Visit((JsSwitchSection)node);
                        break;
                    case JsNodeType.SwitchStatement:
                        Visit((JsSwitchStatement)node);
                        break;
                    case JsNodeType.This:
                        Visit((JsThis)node);
                        break;
                    case JsNodeType.ThrowStatement:
                        Visit((JsThrowStatement)node);
                        break;
                    case JsNodeType.TryStatement:
                        Visit((JsTryStatement)node);
                        break;
                    case JsNodeType.Unit:
                        Visit((JsUnit)node);
                        break;
                    case JsNodeType.VariableDeclarationExpression:
                        Visit((JsVariableDeclarationExpression)node);
                        break;
                    case JsNodeType.VariableDeclarationStatement:
                        Visit((JsVariableDeclarationStatement)node);
                        break;
                    case JsNodeType.VariableDeclarator:
                        Visit((JsVariableDeclarator)node);
                        break;
                    case JsNodeType.WhileStatement:
                        Visit((JsWhileStatement)node);
                        break;
                    case JsNodeType.UseStrictStatement:
                        Visit((JsUseStrictStatement)node);
                        break;
                }
            }

        }

        protected virtual void Visit(JsAssignmentExpression node)
        {
            throw new NotImplementedException("JsAssignmentExpression");
        }

        protected virtual void Visit(JsBinaryExpression node)
        {
            throw new NotImplementedException("JsBinaryExpression");
        }

        protected virtual void Visit(JsBlock node)
        {
            throw new NotImplementedException("JsBlock");
        }

        protected virtual void Visit(JsBreakStatement node)
        {
            throw new NotImplementedException("JsBreakStatement");
        }

        protected virtual void Visit(JsCatchClause node)
        {
            throw new NotImplementedException("JsCatchClause");
        }

        protected virtual void Visit(JsCodeExpression node)
        {
            throw new NotImplementedException("JsCodeExpression");
        }

        protected virtual void Visit(JsCommentStatement node)
        {
            throw new NotImplementedException("JsCommentStatement");
        }

        protected virtual void Visit(JsConditionalExpression node)
        {
            throw new NotImplementedException("JsConditionalExpression");
        }

        protected virtual void Visit(JsContinueStatement node)
        {
            throw new NotImplementedException("JsContinueStatement");
        }

        protected virtual void Visit(JsDoWhileStatement node)
        {
            throw new NotImplementedException("JsDoWhileStatement");
        }

        protected virtual void Visit(JsExpression node)
        {
            throw new NotImplementedException("JsExpression");
        }

        protected virtual void Visit(JsExpressionStatement node)
        {
            throw new NotImplementedException("JsExpressionStatement");
        }

        protected virtual void Visit(JsExternalFileUnit node)
        {
            throw new NotImplementedException("JsExternalFileUnit");
        }

        protected virtual void Visit(JsForInStatement node)
        {
            throw new NotImplementedException("JsForInStatement");
        }

        protected virtual void Visit(JsForStatement node)
        {
            throw new NotImplementedException("JsForStatement");
        }

        protected virtual void Visit(JsFunction node)
        {
            throw new NotImplementedException("JsFunction");
        }

        protected virtual void Visit(JsIfStatement node)
        {
            throw new NotImplementedException("JsIfStatement");
        }

        protected virtual void Visit(JsIndexerAccessExpression node)
        {
            throw new NotImplementedException("JsIndexerAccessExpression");
        }

        protected virtual void Visit(JsInvocationExpression node)
        {
            throw new NotImplementedException("JsInvocationExpression");
        }

        protected virtual void Visit(JsJsonArrayExpression node)
        {
            throw new NotImplementedException("JsJsonArrayExpression");
        }

        protected virtual void Visit(JsJsonMember node)
        {
            throw new NotImplementedException("JsJsonMember");
        }

        protected virtual void Visit(JsJsonNameValue node)
        {
            throw new NotImplementedException("JsJsonNameValue");
        }

        protected virtual void Visit(JsJsonObjectExpression node)
        {
            throw new NotImplementedException("JsJsonObjectExpression");
        }

        protected virtual void Visit(JsMemberExpression node)
        {
            throw new NotImplementedException("JsMemberExpression");
        }

        protected virtual void Visit(JsNewObjectExpression node)
        {
            throw new NotImplementedException("JsNewObjectExpression");
        }

        protected virtual void Visit(JsNodeList node)
        {
            throw new NotImplementedException("JsNodeList");
        }

        protected virtual void Visit(JsNullExpression node)
        {
            throw new NotImplementedException("JsNullExpression");
        }

        protected virtual void Visit(JsNumberExpression node)
        {
            throw new NotImplementedException("JsNumberExpression");
        }

        protected virtual void Visit(JsParenthesizedExpression node)
        {
            throw new NotImplementedException("JsParenthesizedExpression");
        }

        protected virtual void Visit(JsPostUnaryExpression node)
        {
            throw new NotImplementedException("JsPostUnaryExpression");
        }

        protected virtual void Visit(JsPreUnaryExpression node)
        {
            throw new NotImplementedException("JsPreUnaryExpression");
        }

        protected virtual void Visit(JsRegexExpression node)
        {
            throw new NotImplementedException("JsRegexExpression");
        }

        protected virtual void Visit(JsReturnStatement node)
        {
            throw new NotImplementedException("JsReturnStatement");
        }

        protected virtual void Visit(JsStatement node)
        {
            throw new NotImplementedException("JsStatement");
        }

        protected virtual void Visit(JsStatementExpressionList node)
        {
            throw new NotImplementedException("JsStatementExpressionList");
        }

        protected virtual void Visit(JsStringExpression node)
        {
            throw new NotImplementedException("JsStringExpression");
        }

        protected virtual void Visit(JsSwitchLabel node)
        {
            throw new NotImplementedException("JsSwitchLabel");
        }

        protected virtual void Visit(JsSwitchSection node)
        {
            throw new NotImplementedException("JsSwitchSection");
        }

        protected virtual void Visit(JsSwitchStatement node)
        {
            throw new NotImplementedException("JsSwitchStatement");
        }

        protected virtual void Visit(JsThis node)
        {
            throw new NotImplementedException("JsThis");
        }

        protected virtual void Visit(JsThrowStatement node)
        {
            throw new NotImplementedException("JsThrowStatement");
        }

        protected virtual void Visit(JsTryStatement node)
        {
            throw new NotImplementedException("JsTryStatement");
        }

        protected virtual void Visit(JsUnit node)
        {
            throw new NotImplementedException("JsUnit");
        }

        protected virtual void Visit(JsVariableDeclarationExpression node)
        {
            throw new NotImplementedException("JsVariableDeclarationExpression");
        }

        protected virtual void Visit(JsVariableDeclarationStatement node)
        {
            throw new NotImplementedException("JsVariableDeclarationStatement");
        }

        protected virtual void Visit(JsVariableDeclarator node)
        {
            throw new NotImplementedException("JsVariableDeclarator");
        }

        protected virtual void Visit(JsWhileStatement node)
        {
            throw new NotImplementedException("JsWhileStatement");
        }

        protected virtual void Visit(JsUseStrictStatement node)
        {
            throw new NotImplementedException("JsUseStrictStatement");
        }
    }
}
