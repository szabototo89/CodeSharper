using System.Collections.Generic;
using CodeSharper.Core.Common;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.Texts;

namespace CodeSharper.Core.SyntaxTrees
{
    public abstract class Node : IHasChildren<Node>, IHasParent<Node>
    {
        private readonly List<Node> _children;

        /// <summary>
        /// Initializes a new instance of the <see cref="Node"/> class.
        /// </summary>
        protected Node(TextRange textRange)
        {
            Assume.NotNull(textRange, "textRange");

            _children = new List<Node>();
            TextRange = textRange;
            TextDocument = TextRange.TextDocument;
        }

        /// <summary>
        /// Gets the text document of node
        /// </summary>
        public TextDocument TextDocument { get; private set; }

        /// <summary>
        /// Gets or sets the text range of child
        /// </summary>
        public TextRange TextRange { get; protected set; }

        /// <summary>
        /// Gets or sets children of this type
        /// </summary>
        public virtual IEnumerable<Node> Children
        {
            get { return _children.AsReadOnly(); }
        }

        /// <summary>
        /// Appends the specified child to this instance
        /// </summary>
        public virtual void AppendChild(Node child)
        {
            Assume.NotNull(child, "child");

            _children.Add(child);

            child.Parent = this;
        }

        /// <summary>
        /// Attaches this instance to specified node.
        /// </summary>
        public virtual void Attach(Node node)
        {
            Assume.NotNull(node, "node");

            node._children.Add(this);
            Parent = node;
        }

        /// <summary>
        /// Detaches this instance from its parent child
        /// </summary>
        public virtual void Detach()
        {
            if (Parent != null)
                Parent._children.Remove(this);

            Parent = null;
        }

        /// <summary>
        /// Gets or sets parent of current child
        /// </summary>
        public Node Parent { get; protected set; }
    }
}
