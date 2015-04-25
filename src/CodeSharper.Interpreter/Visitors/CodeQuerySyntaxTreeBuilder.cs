using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.SyntaxTrees;
using CodeSharper.Core.Utilities;
using CodeSharper.Interpreter.Common;
using CodeSharper.Interpreter.Utils;

namespace CodeSharper.Interpreter.Visitors
{
    public class CodeQuerySyntaxTreeBuilder : CodeQueryBaseVisitor<Object>
    {
        /// <summary>
        /// Gets or sets the tree factory.
        /// </summary>
        public ICodeQueryCommandFactory TreeFactory { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeQuerySyntaxTreeBuilder"/> class.
        /// </summary>
        public CodeQuerySyntaxTreeBuilder(ICodeQueryCommandFactory treeFactory)
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
        public override Object VisitConstantBoolean(CodeQuery.ConstantBooleanContext context)
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
        public override Object VisitConstantString(CodeQuery.ConstantStringContext context)
        {
            var value = context.STRING().GetText().Trim('"');
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
        public override Object VisitConstantNumber(CodeQuery.ConstantNumberContext context)
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
        public override Object VisitMethodCallParameterValueWithConstant(CodeQuery.MethodCallParameterValueWithConstantContext context)
        {
            var value = context.ActualParameterValue.Accept(this) as Constant;

            if (context.ID() != null)
            {
                var parameterName = context.ParameterName.Text;
                return TreeFactory.ActualParameter(value, parameterName);
            }

            return value;
        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="CodeQuery.methodCall" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// </summary>
        public override Object VisitMethodCall(CodeQuery.MethodCallContext context)
        {
            var methodCallParameters = context.methodCallParameter()
                .Select((parameter, index) => {
                    var param = parameter.Accept(this);
                    if (param is ActualParameter) return param;
                    if (param is Constant) return TreeFactory.ActualParameter((Constant)param, index);

                    return param;
                });

            var methodCall = new {
                Name = context.MethodCallName.Text,
                Parameters = methodCallParameters.OfType<ActualParameter>()
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
        public override Object VisitExpressionMethodCall(CodeQuery.ExpressionMethodCallContext context)
        {
            var methodCall = context.methodCall().Accept(this) as CommandCall;
            return methodCall;
        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="CodeQuery.ExpressionInner" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// </summary>
        public override Object VisitExpressionInner(CodeQuery.ExpressionInnerContext context)
        {
            return context.command().Accept(this);
        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="CodeQuery.command"/>.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
        /// on <paramref name="context"/>.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public override Object VisitCommand(CodeQuery.CommandContext context)
        {
            var methodCall = context.expression().Accept(this).As<CommandCall>();
            var operators = context.COMMAND_OPERATOR().ToArray();

            if (!operators.Any())
            {
                return TreeFactory.ControlFlow(methodCall);
            }

            for (var i = 0; i < operators.Length; i++)
            {
                var op = operators[i];
                var command = context.command().ToArray()[i];

                var pipelineOperator = op.GetText();
                var rightExpression = command.Accept(this) as ControlFlowDescriptorBase;

                if (methodCall != null)
                {
                    return TreeFactory.ControlFlow(pipelineOperator, methodCall, rightExpression);
                }
            }

            return null;
        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="CodeQuery.selectorAttribute" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <returns></returns>
        /// <return>The visitor result.</return>
        public override Object VisitSelectorAttribute(CodeQuery.SelectorAttributeContext context)
        {
            var name = context.AttributeName.Text;
            var value = context.AttributeValue.Accept(this) as Constant;

            return TreeFactory.SelectorElementAttribute(name, value);
        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="CodeQuery.PseudoSelectorWithConstant" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <returns></returns>
        /// <return>The visitor result.</return>
        public override Object VisitPseudoSelectorWithConstant(CodeQuery.PseudoSelectorWithConstantContext context)
        {
            var name = context.Name.Text;
            var value = context.Value.Accept(this) as Constant;

            return TreeFactory.PseudoSelector(name, value);
        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="CodeQuery.PseudoSelectorWithIdentifier" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <returns></returns>
        /// <return>The visitor result.</return>
        public override Object VisitPseudoSelectorWithIdentifier(CodeQuery.PseudoSelectorWithIdentifierContext context)
        {
            var name = context.Name.Text;
            var value = TreeFactory.String(context.Value.Text);

            return TreeFactory.PseudoSelector(name, value);
        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="CodeQuery.selectableElement" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <returns></returns>
        /// <return>The visitor result.</return>
        public override Object VisitSelectableElement(CodeQuery.SelectableElementContext context)
        {
            var isClassElement = context.DOT() != null;
            var name = context.ElementName.Text;
            if (isClassElement)
            {
                name = "." + name;
            }

            var attributes = context.selectorAttribute().AcceptAll(this).Cast<SelectorElementAttribute>();
            var pseudoSelectors = context.pseudoSelector().AcceptAll(this).Cast<PseudoSelector>();

            return TreeFactory.SelectableElement(name, attributes, pseudoSelectors);
        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="CodeQuery.UnarySelection" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// </summary>
        public override Object VisitUnarySelection(CodeQuery.UnarySelectionContext context)
        {
            var element = context.Value.Accept(this).As<SelectableElement>();

            return new UnarySelector(element);
        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="CodeQuery.SelectionWithParenthesis" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// </summary>
        public override Object VisitSelectionWithParenthesis(CodeQuery.SelectionWithParenthesisContext context)
        {
            return context.Selector.Accept(this);
        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="CodeQuery.BinarySelection" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// </summary>
       public override Object VisitBinarySelection(CodeQuery.BinarySelectionContext context)
        {
            var left = context.Left.Accept(this).As<BaseSelector>();
            var right = context.Right.Accept(this).As<BaseSelector>();

            var op = context.SelectorOperator.Safe(value => value.Text) ?? String.Empty;

            switch (op)
            {
                // direct child selector
                case ">":
                    return new BinarySelector(left, right, new DirectChildSelectorOperator());
                // relative child selector
                case "":
                    return new BinarySelector(left, right, new RelativeChildSelectorOperator());
                default:
                    throw new NotSupportedException(String.Format("Not supported child selector: {0}.", op));
            }
        }
    }
}
