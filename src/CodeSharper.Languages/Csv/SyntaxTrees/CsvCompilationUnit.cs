using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.SyntaxTrees;
using CodeSharper.Core.Texts;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Languages.Csv.SyntaxTrees
{
    public class CsvCompilationUnit : CsvNode
    {
        private IEnumerable<RowDeclarationSyntax> _rows;

        /// <summary>
        /// Gets or sets the rows.
        /// </summary>
        public IEnumerable<RowDeclarationSyntax> Rows
        {
            get { return _rows; }
            internal set
            {
                ReplaceChildrenWith(_rows, value);
                _rows = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvCompilationUnit"/> class.
        /// </summary>
        public CsvCompilationUnit(TextRange textRange) : this(textRange, Enumerable.Empty<RowDeclarationSyntax>()) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvCompilationUnit"/> class.
        /// </summary>
        public CsvCompilationUnit(TextRange textRange, IEnumerable<RowDeclarationSyntax> rows)
            : base(textRange)
        {
            Assume.NotNull(rows, "rows");

            Rows = rows;
            this.AppendChildren(rows);
        }
    }
}