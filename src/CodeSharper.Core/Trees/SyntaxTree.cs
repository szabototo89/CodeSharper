using CodeSharper.Core.SyntaxTrees;

namespace CodeSharper.Core.Trees
{
    public class SyntaxTree : Tree<Node> 
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SyntaxTree"/> class.
        /// </summary>
        public SyntaxTree(Node root)
        {
            Root = root;
        }
    }
}