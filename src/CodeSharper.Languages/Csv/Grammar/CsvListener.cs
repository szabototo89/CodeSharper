//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.5
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from D:/Development/Projects/playground/antlr_sample/src\Csv.g4 by ANTLR 4.5

// Unreachable code detected

using Antlr4.Runtime.Misc;

#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591

namespace CodeSharper.Languages.Csv.Grammar {
    using IParseTreeListener = Antlr4.Runtime.Tree.IParseTreeListener;

    /// <summary>
/// This interface defines a complete listener for a parse tree produced by
/// <see cref="CsvParser"/>.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.5")]
[System.CLSCompliant(false)]
public interface ICsvListener : IParseTreeListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="CsvParser.start"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStart([NotNull] CsvParser.StartContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="CsvParser.start"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStart([NotNull] CsvParser.StartContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="CsvParser.row"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterRow([NotNull] CsvParser.RowContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="CsvParser.row"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitRow([NotNull] CsvParser.RowContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="CsvParser.field"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterField([NotNull] CsvParser.FieldContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="CsvParser.field"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitField([NotNull] CsvParser.FieldContext context);
}
} // namespace Grammar