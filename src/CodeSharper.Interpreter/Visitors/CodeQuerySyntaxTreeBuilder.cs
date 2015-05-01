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
        /// Gets or sets the node selector factory.
        /// </summary>
        public INodeSelectorFactory NodeSelectorFactory { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeQuerySyntaxTreeBuilder"/> class.
        /// </summary>
        public CodeQuerySyntaxTreeBuilder(INodeSelectorFactory nodeSelectorFactory, ICodeQueryCommandFactory treeFactory)
        {
            Assume.NotNull(treeFactory, "treeFactory");
            Assume.NotNull(nodeSelectorFactory, "nodeSelectorFactory");
            TreeFactory = treeFactory;
            NodeSelectorFactory = nodeSelectorFactory;
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
                    return TreeFactory.CreateBoolean(false);
                    break;
                case "true":
                    return TreeFactory.CreateBoolean(true);
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
            return TreeFactory.CreateString(value);
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
            return TreeFactory.CreateNumber(value);
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
                return TreeFactory.CreateActualParameter(value, parameterName);
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
                    if (param is Constant) return TreeFactory.CreateActualParameter((Constant)param, index);

                    return param;
                });

            var methodCall = new {
                Name = context.MethodCallName.Text,
                Parameters = methodCallParameters.OfType<ActualParameter>()
                                                 .ToArray()
            };

            return TreeFactory.CreateMethodCall(methodCall.Name, methodCall.Parameters);
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
            var methodCall = context.methodCall().Accept(this).As<CommandCall>();
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


        /*
                public override Object VisitCommand(CodeQuery.CommandContext context)
                {
                    var expression = context.expression().Accept(this);

                    if (expression is CommandCall)
                    {
                        var methodCall = (CommandCall)expression;
                        var operators = context.COMMAND_OPERATOR().ToArray();

                        if (!operators.Any())
                        {
                            return TreeFactory.CreateControlFlow(methodCall);
                        }

                        for (var i = 0; i < operators.Length; i++)
                        {
                            var op = operators[i];
                            var command = context.command().ToArray()[i];

                            var pipelineOperator = op.GetText();
                            var rightExpression = command.Accept(this) as ControlFlowDescriptorBase;

                            if (methodCall != null)
                            {
                                return TreeFactory.CreateControlFlow(pipelineOperator, methodCall, rightExpression);
                            }
                        }
                    }
                    else if (expression is BaseSelector)
                    {
                        var selector = ((BaseSelector)expression);
                        return TreeFactory.CreateControlFlow(selector);
                    }

                    return null;
                }
        */

        public override Object VisitCommandOperand(CodeQuery.CommandOperandContext context)
        {
            var left = context.Left.Accept(this) as ControlFlowDescriptorBase;
            var right = context.Right.Accept(this) as ControlFlowDescriptorBase;
            var pipelineOperator = context.Operator.Text;
            
            return TreeFactory.CreateControlFlow(left, right, pipelineOperator);
        }

        public override Object VisitCommandExpression(CodeQuery.CommandExpressionContext context)
        {
            var expression = context.Expression.Accept(this);

            if (expression is CommandCall)
            {
                var methodCall = (CommandCall)expression;
                return TreeFactory.CreateControlFlow(methodCall);
            }
            else if (expression is BaseSelector)
            {
                var selector = (BaseSelector)expression;
                return TreeFactory.CreateControlFlow(selector);
            }

            return base.VisitCommandExpression(context);
        }

        public override Object VisitCommandInner(CodeQuery.CommandInnerContext context)
        {
            return context.Command.Accept(this);
        }

        #region Code selection language feature

        /// <summary>
        /// Visit a parse tree produced by <see cref="CodeQuery.ExpressionSelector"/>.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
        /// on <paramref name="context"/>.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public override Object VisitExpressionSelector(CodeQuery.ExpressionSelectorContext context)
        {
            var expression = context.selector().Accept(this).As<BaseSelector>();
            return expression;
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

            return NodeSelectorFactory.CreateAttributeSelector(name, value);
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

            return NodeSelectorFactory.CreatePseudoSelector(name, value);
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
            var value = TreeFactory.CreateString(context.Value.Text);

            return NodeSelectorFactory.CreatePseudoSelector(name, value);
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

            var attributes = context.selectorAttribute().AcceptAll(this).Cast<AttributeSelector>();
            var pseudoSelectors = context.pseudoSelector().AcceptAll(this).Cast<PseudoSelector>();

            return NodeSelectorFactory.CreateElementTypeSelector(name, attributes, pseudoSelectors);
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
            var element = context.Value.Accept(this).Cast<ElementTypeSelector>();

            return NodeSelectorFactory.CreateUnarySelector(element);
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

            var @operator = context.SelectorOperator.Safe(value => value.Text) ?? String.Empty;
            var selectorOperator = NodeSelectorFactory.CreateCombinator(@operator);

            return NodeSelectorFactory.CreateBinarySelector(left, right, selectorOperator);
        }

        #endregion

    }
}
