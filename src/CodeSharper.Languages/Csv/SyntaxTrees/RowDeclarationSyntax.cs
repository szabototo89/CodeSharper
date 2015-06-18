using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using CodeSharper.Core.SyntaxTrees;
using CodeSharper.Core.Texts;
using CodeSharper.Core.Transformation;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Languages.Csv.SyntaxTrees
{
    public class RowDeclarationSyntax : CsvNode, ICanRemove
    {
        private IEnumerable<CommaToken> commas;
        private IEnumerable<FieldDeclarationSyntax> fields;

        /// <summary>
        /// Initializes a new instance of the <see cref="RowDeclarationSyntax"/> class.
        /// </summary>
        public RowDeclarationSyntax(TextRange textRange)
            : base(textRange)
        {
        }

        /// <summary>
        /// Gets the commas.
        /// </summary>
        public IEnumerable<CommaToken> Commas
        {
            get { return commas; }
            internal set
            {
                ReplaceChildrenWith(commas, value);
                commas = value;
            }
        }

        /// <summary>
        /// Gets the fields.
        /// </summary>
        public IEnumerable<FieldDeclarationSyntax> Fields
        {
            get { return fields; }
            internal set
            {
                ReplaceChildrenWith(commas, value);
                fields = value;
            }
        }

        /// <summary>
        /// Removes this instance.
        /// </summary>
        public Boolean Remove()
        {
            var document = Parent as CsvCompilationUnit;
            if (document == null) return false;

            var previousValues = document.Rows.ToArray();
            document.Rows = previousValues.Where(value => !ReferenceEquals(value, this)).ToArray();
            TextRange.ChangeText("");

            return true;
        }
    }
}