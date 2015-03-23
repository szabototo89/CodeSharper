using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.SyntaxTrees;
using CodeSharper.Core.Texts;
using CodeSharper.Core.Trees;
using CodeSharper.Languages.Csv.SyntaxTrees;

namespace CodeSharper.Languages.Csv.Factories
{
    public class CsvStandardTreeFactory : ICsvTreeFactory
    {
        private readonly Stack<CsvCompilationUnit> _csvCompilationUnits;
        private RowSyntax _actualRow;

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvStandardTreeFactory"/> class.
        /// </summary>
        public CsvStandardTreeFactory()
        {
            _csvCompilationUnits = new Stack<CsvCompilationUnit>();
        }

        /// <summary>
        /// Creates row node and appends it to actual document node
        /// </summary>
        public ICsvTreeFactory CreateRow(TextRange textRange)
        {
            Assume.NotNull(textRange, "textRange");
            checkIsDocumentDefined();
            _actualRow = new RowSyntax(textRange);
            _csvCompilationUnits.Peek().AppendChild(_actualRow);
            return this;
        }

        /// <summary>
        /// Creates field and appends it to actual row
        /// </summary>
        public ICsvTreeFactory CreateField(TextRange textRange)
        {
            Assume.NotNull(textRange, "textRange");
            checkIsRowAdded();

            var field = new FieldSyntax(textRange);
            addChildToLastDefinedRow(field);

            return this;
        }

        /// <summary>
        /// Creates comma and appends it to actual row
        /// </summary>
        /// <param name="textRange">The text range.</param>
        public ICsvTreeFactory CreateComma(TextRange textRange)
        {
            Assume.NotNull(textRange, "textRange");
            checkIsRowAdded();

            var comma = new CommaToken(textRange);
            addChildToLastDefinedRow(comma);

            return this;
        }

        /// <summary>
        /// Creates the document node 
        /// </summary>
        public ICsvTreeFactory CreateDocument(TextRange textRange)
        {
            Assume.NotNull(textRange, "textRange");
            var csvCompilationUnit = new CsvCompilationUnit(textRange);

            _csvCompilationUnits.Push(csvCompilationUnit);

            return this;
        }

        public TextDocument GetTextDocument()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the built syntax tree
        /// </summary>
        public SyntaxTree GetSyntaxTree()
        {
            checkIsDocumentDefined();

            var document = _csvCompilationUnits.Peek();

            return new SyntaxTree(document);
        }

        #region Helper methods for creating tree

        private void addChildToLastDefinedRow(Node field)
        {
            _actualRow.AppendChild(field);
        }

        #endregion

        #region Helper methods for error checking

        private void checkIsDocumentDefined()
        {
            if (!_csvCompilationUnits.Any())
                throw ThrowHelper.Exception("Document is not defined!");
        }

        [DebuggerStepThrough]
        private void checkIsRowAdded()
        {
            if (_actualRow == null)
                throw ThrowHelper.Exception("Row is not defined!");
        }

        #endregion
    }
}