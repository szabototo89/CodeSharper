using System.Collections.Generic;
using CodeSharper.Core.Texts;
using CodeSharper.Languages.Csv.SyntaxTrees;

namespace CodeSharper.Languages.Csv.Factories
{
    public interface ICsvSyntaxTreeFactory
    {

        /// <summary>
        /// Creates a field with the specified text range.
        /// </summary>
        FieldDeclarationSyntax Field(TextRange textRange);

        /// <summary>
        /// Creates a row with the specified text range, commas and fields
        /// </summary>
        RowDeclarationSyntax Row(TextRange textRange, IEnumerable<CommaToken> commas, IEnumerable<FieldDeclarationSyntax> fields);

        /// <summary>
        /// Creates a compilation unit with the specified text range and rows
        /// </summary>
        CsvCompilationUnit CompilationUnit(TextRange textRange, IEnumerable<RowDeclarationSyntax> rows);
    }
}