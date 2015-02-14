using CodeSharper.Core.SyntaxTrees;
using CodeSharper.Core.Texts;

namespace CodeSharper.Languages.Csv.SyntaxTrees
{
    public class CommaNode : LeafNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommaNode"/> class.
        /// </summary>
        public CommaNode(TextRange textRange)
            : base(textRange)
        { }
    }
}