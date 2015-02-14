using CodeSharper.Core.SyntaxTrees;
using CodeSharper.Core.Texts;

namespace CodeSharper.Languages.Csv.SyntaxTrees
{
    public class CsvDocumentNode : Node
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CsvDocumentNode"/> class.
        /// </summary>
        public CsvDocumentNode(TextRange textRange) : base(textRange) { }
    }
}