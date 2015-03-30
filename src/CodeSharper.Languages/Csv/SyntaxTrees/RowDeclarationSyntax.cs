using System.Collections.Generic;
using CodeSharper.Core.SyntaxTrees;
using CodeSharper.Core.Texts;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Languages.Csv.SyntaxTrees
{
    public class RowDeclarationSyntax : CsvNode
    {
        private IEnumerable<CommaToken> _commas;
        private IEnumerable<FieldDeclarationSyntax> _fields;

        /// <summary>
        /// Initializes a new instance of the <see cref="RowDeclarationSyntax"/> class.
        /// </summary>
        public RowDeclarationSyntax(TextRange textRange) 
            : base(textRange)
        {
        }

        public IEnumerable<CommaToken> Commas
        {
            get { return _commas; }
            internal set
            {
                ReplaceChildrenWith(_commas, value);
                _commas = value;
            }
        }

        public IEnumerable<FieldDeclarationSyntax> Fields
        {
            get { return _fields; }
            internal set
            {
                ReplaceChildrenWith(_commas, value);
                _fields = value;
            }
        }
    }
}