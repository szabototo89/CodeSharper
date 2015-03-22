using CodeSharper.Core.SyntaxTrees;
using CodeSharper.Core.Texts;

namespace CodeSharper.Languages.Csv.SyntaxTrees
{
    public class RowNode : Node
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RowNode"/> class.
        /// </summary>
        public RowNode(TextRange textRange) 
            : base(textRange)
        {
        }
    }
}