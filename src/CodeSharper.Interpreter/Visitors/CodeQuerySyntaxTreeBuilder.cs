using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.SyntaxTrees;
using CodeSharper.Core.Utilities;
using CodeSharper.Interpreter.Common;
using CodeSharper.Interpreter.Grammar;
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
        /// Gets or sets the node selectorElement factory.
        /// </summary>
        public ISelectorFactory SelectorFactory { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeQuerySyntaxTreeBuilder"/> class.
        /// </summary>
        public CodeQuerySyntaxTreeBuilder(ISelectorFactory selectorFactory, ICodeQueryCommandFactory treeFactory)
        {
            Assume.NotNull(treeFactory, "treeFactory");
            Assume.NotNull(selectorFactory, "SelectorFactory");
            TreeFactory = treeFactory;
            SelectorFactory = selectorFactory;
        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="CodeQueryParser.ConstantBoolean" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// </summary>
        public override Object VisitConstantBoolean(CodeQueryParser.ConstantBooleanContext context)
        {
            switch (context.BOOLEAN().GetText())
            {
                case "false":
                    return TreeFactory.CreateBoolean(false);
                case "true":
                    return TreeFactory.CreateBoolean(true);
                default:
                    throw new NotSupportedException("Not supported boolean value!");
            }
        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="CodeQueryParser.ConstantString" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// </summary>
        public override Object VisitConstantString(CodeQueryParser.ConstantStringContext context)
        {
            var value = context.STRING().GetText().Trim('"');
            return TreeFactory.CreateString(value);
        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="CodeQueryParser.ConstantNumber" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        public override Object VisitConstantNumber(CodeQueryParser.ConstantNumberContext context)
        {
            Double value;
            if (!Double.TryParse(context.NUMBER().GetText(), out value))
                throw new Exception("Invalid number!");
            return TreeFactory.CreateNumber(value);
        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="CodeQueryParser.MethodCallParameterValueWithConstant" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// </summary>
        public override Object VisitMethodCallParameterValueWithConstant(CodeQueryParser.MethodCallParameterValueWithConstantContext context)
        {
            var value = context.ActualParameterValue.Accept(this) as ConstantElement;

            if (context.ID() != null)
            {
                var parameterName = context.ParameterName.Text;
                return TreeFactory.CreateActualParameter(value, parameterName);
            }

            return value;
        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="CodeQueryParser.methodCall" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// </summary>
        public override Object VisitMethodCall(CodeQueryParser.MethodCallContext context)
        {
            var parameters = context.methodCallParameter();
            var methodCallParameters = parameters.Select((parameter, index) => {
                var param = parameter.Accept(this);
                if (param is ActualParameterElement) return param;
                if (param is ConstantElement)
                    return TreeFactory.CreateActualParameter((ConstantElement) param, index);

                return param;
            });

            var methodCall = new
            {
                Name = context.MethodCallName.Text,
                Parameters = methodCallParameters.OfType<ActualParameterElement>()
                                                 .ToArray()
            };

            return TreeFactory.CreateMethodCall(methodCall.Name, methodCall.Parameters);
        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="CodeQueryParser.ExpressionMethodCall" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// </summary>
        public override Object VisitExpressionMethodCall(CodeQueryParser.ExpressionMethodCallContext context)
        {
            var methodCall = context.methodCall().Accept(this).As<CommandCallElement>();
            return methodCall;
        }

        /*
                public override Object VisitCommand(CodeQuery.CommandContext context)
                {
                    var expression = context.expression().Accept(this);

                    if (expression is CommandCallElement)
                    {
                        var methodCallElement = (CommandCallElement)expression;
                        var operators = context.COMMAND_OPERATOR().ToArray();

                        if (!operators.Any())
                        {
                            return TreeFactory.CreateControlFlow(methodCallElement);
                        }

                        for (var i = 0; i < operators.Length; i++)
                        {
                            var op = operators[i];
                            var command = context.command().ToArray()[i];

                            var pipelineOperator = op.GetText();
                            var rightExpression = command.Accept(this) as ControlFlowElementBase;

                            if (methodCallElement != null)
                            {
                                return TreeFactory.CreateControlFlow(pipelineOperator, methodCallElement, rightExpression);
                            }
                        }
                    }
                    else if (expression is BaseSelectorElement)
                    {
                        var selectorElement = ((BaseSelectorElement)expression);
                        return TreeFactory.CreateControlFlow(selectorElement);
                    }

                    return null;
                }
        */

        public override Object VisitCommandOperand(CodeQueryParser.CommandOperandContext context)
        {
            var left = context.Left.Accept(this) as ControlFlowElementBase;
            var right = context.Right.Accept(this) as ControlFlowElementBase;
            var pipelineOperator = context.Operator.Text;

            return TreeFactory.CreateControlFlow(left, right, pipelineOperator);
        }

        public override Object VisitCommandExpression(CodeQueryParser.CommandExpressionContext context)
        {
            var expression = context.Expression.Accept(this);

            if (expression is CommandCallElement)
            {
                var methodCall = (CommandCallElement) expression;
                return TreeFactory.CreateControlFlow(methodCall);
            }
            else if (expression is SelectorElementBase)
            {
                var selector = (SelectorElementBase) expression;
                return TreeFactory.CreateControlFlow(selector);
            }

            return base.VisitCommandExpression(context);
        }

        public override Object VisitCommandInner(CodeQueryParser.CommandInnerContext context)
        {
            return context.Command.Accept(this);
        }

        #region Code selection language feature

        /// <summary>
        /// Visit a parse tree produced by <see cref="CodeQueryParser.ExpressionSelector"/>.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
        /// on <paramref name="context"/>.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public override Object VisitExpressionSelector(CodeQueryParser.ExpressionSelectorContext context)
        {
            var expression = context.selector().Accept(this).As<SelectorElementBase>();
            return expression;
        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="CodeQueryParser.selectorAttribute" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <returns></returns>
        /// <return>The visitor result.</return>
        public override Object VisitSelectorAttribute(CodeQueryParser.SelectorAttributeContext context)
        {
            var name = context.AttributeName.Text;
            var value = context.AttributeValue
                               .Safe(attributeValue => attributeValue.Accept(this) as ConstantElement);

            return SelectorFactory.CreateAttributeSelector(name, value);
        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="CodeQueryParser.PseudoSelectorWithConstant" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <returns></returns>
        /// <return>The visitor result.</return>
        public override Object VisitPseudoSelectorWithConstant(CodeQueryParser.PseudoSelectorWithConstantContext context)
        {
            var name = context.Name.Text;
            IEnumerable<ConstantElement> values = Enumerable.Empty<ConstantElement>();

            if (context.constant() != null)
                values = context.constant().AcceptAll(this).OfType<ConstantElement>().ToArray();

            return SelectorFactory.CreateModifier(name, values);
        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="CodeQueryParser.PseudoSelectorWithIdentifier" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <returns></returns>
        /// <return>The visitor result.</return>
        public override Object VisitPseudoSelectorWithIdentifier(CodeQueryParser.PseudoSelectorWithIdentifierContext context)
        {
            var name = context.Name.Text;
            var values = Enumerable.Empty<ConstantElement>();

            if (context.ID() != null)
                values = context.ID().Select(id => TreeFactory.CreateString(id.GetText()));

            return SelectorFactory.CreateModifier(name, values);
        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="CodeQueryParser.PseudoSelectorWithSelector"/>.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
        /// on <paramref name="context"/>.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public override Object VisitPseudoSelectorWithSelector(CodeQueryParser.PseudoSelectorWithSelectorContext context)
        {
            var name = context.Name.Text;
            var selector = context.Value.Accept(this) as SelectorElementBase;

            return SelectorFactory.CreateModifier(name, new[] {selector});
        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="CodeQueryParser.selectableElement" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <returns></returns>
        /// <return>The visitor result.</return>
        public override Object VisitSelectableElement(CodeQueryParser.SelectableElementContext context)
        {
            var isClassElement = context.DOT() != null;
            var name = context.ElementName.Text;
            if (isClassElement)
                name = "." + name;

            var attributes = context.selectorAttribute().AcceptAll(this).Cast<AttributeElement>();
            var pseudoSelectors = context.pseudoSelector().AcceptAll(this).Cast<ModifierElement>();
            var classSelectors = context.className().AcceptAll(this).Cast<ClassSelectorElement>();

            return SelectorFactory.CreateElementTypeSelector(name, attributes, pseudoSelectors, classSelectors);
        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="CodeQueryParser.className"/>.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)"/>
        /// on <paramref name="context"/>.
        /// </para>
        /// </summary>
        /// <param name="context">The parse tree.</param>
        /// <return>The visitor result.</return>
        public override Object VisitClassName(CodeQueryParser.ClassNameContext context)
        {
            var id = context.ID();
            if (id != null)
                return SelectorFactory.CreateClassSelectorElement(id.GetText(), false);

            var stringId = context.STRING();
            if (stringId != null)
            {
                var regularExpression = stringId.GetText()
                                                .Replace("?", ".")
                                                .Replace("*", ".?");

                return SelectorFactory.CreateClassSelectorElement(regularExpression, true);
            }

            var regularExpressionId = context.REGULAR_EXPRESSION();
            if (regularExpressionId != null)
            {
                var regularExpression = regularExpressionId.GetText();
                regularExpression = regularExpression.Substring(1, regularExpression.Length - 2);
                return SelectorFactory.CreateClassSelectorElement(regularExpression, true);
            }

            throw new NotSupportedException("Not supported class name format.");
        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="CodeQueryParser.UnarySelection" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// </summary>
        public override Object VisitUnarySelection(CodeQueryParser.UnarySelectionContext context)
        {
            var element = context.Value.Accept(this).Cast<TypeSelectorElement>();

            return SelectorFactory.CreateUnarySelector(element);
        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="CodeQueryParser.SelectionWithParenthesis" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// </summary>
        public override Object VisitSelectionWithParenthesis(CodeQueryParser.SelectionWithParenthesisContext context)
        {
            return context.Selector.Accept(this);
        }

        /// <summary>
        /// Visit a parse tree produced by <see cref="CodeQueryParser.BinarySelection" />.
        /// <para>
        /// The default implementation returns the result of calling <see cref="AbstractParseTreeVisitor{Result}.VisitChildren(IRuleNode)" />
        /// on <paramref name="context" />.
        /// </para>
        /// </summary>
        public override Object VisitBinarySelection(CodeQueryParser.BinarySelectionContext context)
        {
            var left = context.Left.Accept(this).As<SelectorElementBase>();
            var right = context.Right.Accept(this).As<SelectorElementBase>();

            var @operator = context.SelectorOperator.Safe(value => value.Text) ?? String.Empty;
            var selectorOperator = SelectorFactory.CreateCombinator(@operator);

            return SelectorFactory.CreateBinarySelector(left, right, selectorOperator);
        }

        #endregion
    }
}