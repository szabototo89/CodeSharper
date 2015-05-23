using System.Collections.Generic;
using CodeSharper.Core.Texts;

namespace CodeSharper.Core.SyntaxTrees
{
    public abstract class SyntaxToken : Node, ILeaf
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SyntaxToken"/> class.
        /// </summary>
        protected SyntaxToken(TextRange textRange)
            : base(textRange)
        {
        }

        /// <summary>
        /// Appends the specified child to this instance
        /// </summary>
        public sealed override void AppendChild(Node child) { }

        /// <summary>
        /// Gets or sets children of this type
        /// </summary>
        public sealed override IEnumerable<Node> Children
        {
            get { return base.Children; }
        }
    }
}