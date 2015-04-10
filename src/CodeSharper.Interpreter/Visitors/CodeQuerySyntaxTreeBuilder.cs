using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Tree;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.SyntaxTrees;
using CodeSharper.Core.Utilities;
using CodeSharper.Interpreter.Nodes;

namespace CodeSharper.Interpreter.Visitors
{
    public class CodeQuerySyntaxTreeBuilder : CodeQueryBaseVisitor<CodeQueryNode>
    {
        /// <summary>
        /// Gets or sets the tree factory.
        /// </summary>
        public ICodeQuerySyntaxTreeFactory TreeFactory { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeQuerySyntaxTreeBuilder"/> class.
        /// </summary>
        public CodeQuerySyntaxTreeBuilder(ICodeQuerySyntaxTreeFactory treeFactory)
        {
            Assume.NotNull(treeFactory, "treeFactory");
            TreeFactory = treeFactory;
        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="CodeQuery.ConstantBoolean" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// </summary>
        public override CodeQueryNode VisitConstantBoolean(CodeQuery.ConstantBooleanContext context)
        {
            switch (context.BOOLEAN().GetText())
            {
                case "false":
                    return TreeFactory.Boolean(false);
                    break;
                case "true":
                    return TreeFactory.Boolean(true);
                    break;
                default:
                    throw new NotSupportedException("Not supported boolean value!");
            }
        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="CodeQuery.ConstantString" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// </summary>
        public override CodeQueryNode VisitConstantString(CodeQuery.ConstantStringContext context)
        {
            var value = context.String().GetText().Trim('"');
            return TreeFactory.String(value);
        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="CodeQuery.ConstantNumber" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public override CodeQueryNode VisitConstantNumber(CodeQuery.ConstantNumberContext context)
        {
            Double value;
            if (!Double.TryParse(context.NUMBER().GetText(), out value))
            {
                throw new Exception("Invalid number!");
            }
            return TreeFactory.Number(value);
        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="CodeQuery.MethodCallParameterValueWithConstant" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// </summary>
        public override CodeQueryNode VisitMethodCallParameterValueWithConstant(CodeQuery.MethodCallParameterValueWithConstantContext context)
        {
            var value = context.ActualParameterValue.Accept(this);
            return value;
        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="CodeQuery.methodCall" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// </summary>
        public override CodeQueryNode VisitMethodCall(CodeQuery.MethodCallContext context)
        {
            var methodCall = new {
                Name = context.MethodCallName.Text,
                Parameters = context.methodCallParameter()
                                    .Select(parameter => parameter.Accept(this))
                                    .OfType<ActualParameterSyntax>()
                                    .ToArray()
            };

            return TreeFactory.MethodCall(methodCall.Name, methodCall.Parameters);
        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="CodeQuery.ExpressionMethodCall" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// </summary>
        public override CodeQueryNode VisitExpressionMethodCall(CodeQuery.ExpressionMethodCallContext context)
        {
            var methodCall = context.methodCall().Accept(this) as MethodCallSymbol;
            return methodCall;
        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="CodeQuery.ExpressionInner" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// </summary>
        public override CodeQueryNode VisitExpressionInner(CodeQuery.ExpressionInnerContext context)
        {
            return context.expression().Accept(this);
        }

        public override CodeQueryNode VisitCommand(CodeQuery.CommandContext context)
        {
            var pipelineOperator = context.PIPELINE_OPERATOR().GetText();

            var rightExpression = context.command().Accept(this) as ControlFlowSymbol;

            var methodCall = context.expression().Accept(this).As<MethodCallSymbol>();
            if (methodCall != null)
            {
                return TreeFactory.ControlFlow(pipelineOperator, methodCall, rightExpression);    
            }
        }
    }
}
