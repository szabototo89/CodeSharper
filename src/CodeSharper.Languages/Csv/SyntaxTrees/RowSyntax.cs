using System.Collections.Generic;
using CodeSharper.Core.SyntaxTrees;
using CodeSharper.Core.Texts;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Languages.Csv.SyntaxTrees
{
    public class RowSyntax : CsvNode
    {
        private IEnumerable<CommaToken> _commas;
        private IEnumerable<FieldSyntax> _fields;

        /// <summary>
        /// Initializes a new instance of the <see cref="RowSyntax"/> class.
        /// </summary>
        public RowSyntax(TextRange textRange) 
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

        public IEnumerable<FieldSyntax> Fields
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