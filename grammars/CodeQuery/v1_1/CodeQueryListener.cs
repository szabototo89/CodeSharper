//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.5
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from D:/Development/Projects/antlr/CodeSharper-grammars/src/CodeQuery/v1_1\CodeQuery.g4 by ANTLR 4.5

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591

using Antlr4.Runtime.Misc;
using IParseTreeListener = Antlr4.Runtime.Tree.IParseTreeListener;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete listener for a parse tree produced by
/// <see cref="CodeQuery"/>.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.5")]
[System.CLSCompliant(false)]
public interface ICodeQueryListener : IParseTreeListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="CodeQuery.command"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterCommand([NotNull] CodeQuery.CommandContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="CodeQuery.command"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitCommand([NotNull] CodeQuery.CommandContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>ExpressionMethodCall</c>
	/// labeled alternative in <see cref="CodeQuery.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpressionMethodCall([NotNull] CodeQuery.ExpressionMethodCallContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>ExpressionMethodCall</c>
	/// labeled alternative in <see cref="CodeQuery.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpressionMethodCall([NotNull] CodeQuery.ExpressionMethodCallContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>ExpressionSelector</c>
	/// labeled alternative in <see cref="CodeQuery.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpressionSelector([NotNull] CodeQuery.ExpressionSelectorContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>ExpressionSelector</c>
	/// labeled alternative in <see cref="CodeQuery.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpressionSelector([NotNull] CodeQuery.ExpressionSelectorContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>ExpressionInBrackets</c>
	/// labeled alternative in <see cref="CodeQuery.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpressionInBrackets([NotNull] CodeQuery.ExpressionInBracketsContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>ExpressionInBrackets</c>
	/// labeled alternative in <see cref="CodeQuery.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpressionInBrackets([NotNull] CodeQuery.ExpressionInBracketsContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="CodeQuery.methodCall"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterMethodCall([NotNull] CodeQuery.MethodCallContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="CodeQuery.methodCall"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitMethodCall([NotNull] CodeQuery.MethodCallContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>MethodCallParameterValueWithExpression</c>
	/// labeled alternative in <see cref="CodeQuery.methodCallParameter"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterMethodCallParameterValueWithExpression([NotNull] CodeQuery.MethodCallParameterValueWithExpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>MethodCallParameterValueWithExpression</c>
	/// labeled alternative in <see cref="CodeQuery.methodCallParameter"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitMethodCallParameterValueWithExpression([NotNull] CodeQuery.MethodCallParameterValueWithExpressionContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>MethodCallParameterValueWithConstant</c>
	/// labeled alternative in <see cref="CodeQuery.methodCallParameter"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterMethodCallParameterValueWithConstant([NotNull] CodeQuery.MethodCallParameterValueWithConstantContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>MethodCallParameterValueWithConstant</c>
	/// labeled alternative in <see cref="CodeQuery.methodCallParameter"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitMethodCallParameterValueWithConstant([NotNull] CodeQuery.MethodCallParameterValueWithConstantContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>MethodCallParameterValueWithIdentifier</c>
	/// labeled alternative in <see cref="CodeQuery.methodCallParameter"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterMethodCallParameterValueWithIdentifier([NotNull] CodeQuery.MethodCallParameterValueWithIdentifierContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>MethodCallParameterValueWithIdentifier</c>
	/// labeled alternative in <see cref="CodeQuery.methodCallParameter"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitMethodCallParameterValueWithIdentifier([NotNull] CodeQuery.MethodCallParameterValueWithIdentifierContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="CodeQuery.selector"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterSelector([NotNull] CodeQuery.SelectorContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="CodeQuery.selector"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitSelector([NotNull] CodeQuery.SelectorContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="CodeQuery.selectableElement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterSelectableElement([NotNull] CodeQuery.SelectableElementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="CodeQuery.selectableElement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitSelectableElement([NotNull] CodeQuery.SelectableElementContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>PseudoSelectorWithConstant</c>
	/// labeled alternative in <see cref="CodeQuery.pseudoSelector"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPseudoSelectorWithConstant([NotNull] CodeQuery.PseudoSelectorWithConstantContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>PseudoSelectorWithConstant</c>
	/// labeled alternative in <see cref="CodeQuery.pseudoSelector"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPseudoSelectorWithConstant([NotNull] CodeQuery.PseudoSelectorWithConstantContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>PseudoSelectorWithIdentifier</c>
	/// labeled alternative in <see cref="CodeQuery.pseudoSelector"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPseudoSelectorWithIdentifier([NotNull] CodeQuery.PseudoSelectorWithIdentifierContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>PseudoSelectorWithIdentifier</c>
	/// labeled alternative in <see cref="CodeQuery.pseudoSelector"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPseudoSelectorWithIdentifier([NotNull] CodeQuery.PseudoSelectorWithIdentifierContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="CodeQuery.selectorAttribute"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterSelectorAttribute([NotNull] CodeQuery.SelectorAttributeContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="CodeQuery.selectorAttribute"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitSelectorAttribute([NotNull] CodeQuery.SelectorAttributeContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="CodeQuery.constant"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterConstant([NotNull] CodeQuery.ConstantContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="CodeQuery.constant"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitConstant([NotNull] CodeQuery.ConstantContext context);
}
