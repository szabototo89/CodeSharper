﻿using System.Collections.Generic;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.Texts;
using CodeSharper.Languages.Csv.SyntaxTrees;

namespace CodeSharper.Languages.Csv.Factories
{
    public class CsvSyntaxTreeFactory : ICsvSyntaxTreeFactory
    {
        private static ICsvSyntaxTreeFactory _instance;

        /// <summary>
        /// Gets a singleton instance of <see cref="CsvSyntaxTreeFactory"/>
        /// </summary>
        public static ICsvSyntaxTreeFactory Instance
        {
            get
            {
                if (_instance == null)
                    return new CsvSyntaxTreeFactory();
                return _instance;
            }
        }

        public FieldDeclarationSyntax Field(TextRange textRange)
        {
            Assume.NotNull(textRange, "textRange");
            return new FieldDeclarationSyntax(textRange);
        }

        public RowDeclarationSyntax Row(TextRange textRange, IEnumerable<CommaToken> commas, IEnumerable<FieldDeclarationSyntax> fields)
        {
            Assume.NotNull(commas, "commas");
            Assume.NotNull(fields, "fields");

            return new RowDeclarationSyntax(textRange) {
                Commas = commas,
                Fields = fields
            };
        }

        public CsvCompilationUnit CompilationUnit(TextRange textRange, IEnumerable<RowDeclarationSyntax> rows)
        {
            Assume.NotNull(textRange, "textRange");
            Assume.NotNull(rows, "rows");

            return new CsvCompilationUnit(textRange, rows);
        }
    }
}
