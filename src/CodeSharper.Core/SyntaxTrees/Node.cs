using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Common;
using CodeSharper.Core.Common.Interfaces;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.Texts;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Core.SyntaxTrees
{
    public abstract class Node : IHasChildren<Node>, IHasParent<Node>, IHasTextRange
    {
        private readonly List<Node> children;

        /// <summary>
        /// Initializes a new instance of the <see cref="Node"/> class.
        /// </summary>
        protected Node(TextRange textRange)
        {
            Assume.NotNull(textRange, nameof(textRange));

            children = new List<Node>();
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
            get { return children.AsReadOnly(); }
        }

        /// <summary>
        /// Appends the specified child to this instance
        /// </summary>
        public virtual void AppendChild(Node child)
        {
            Assume.NotNull(child, nameof(child));
            children.Add(child);
            child.Parent = this;
        }

        /// <summary>
        /// Attaches this instance to specified node.
        /// </summary>
        public virtual void Attach(Node node)
        {
            Assume.NotNull(node, nameof(node));

            node.children.Add(this);
            Parent = node;
        }

        /// <summary>
        /// Detaches this instance from its parent child
        /// </summary>
        public virtual void Detach()
        {
            if (Parent != null)
                Parent.children.Remove(this);

            Parent = null;
        }

        protected void ReplaceChildWith(Node oldChild, Node newChild)
        {
            if (oldChild != null)
                oldChild.Detach();
            if (newChild != null)
                newChild.Attach(this);
        }

        protected void ReplaceChildrenWith(IEnumerable<Node> oldChildren, IEnumerable<Node> newChildren)
        {
            if (oldChildren == null) return;
            foreach (var child in oldChildren.WhereNotNull())
            {
                child.Detach();
            }

            if (newChildren == null) return;
            foreach (var child in newChildren.WhereNotNull())
            {
                child.Attach(this);
            }
        }

        /// <summary>
        /// Gets or sets parent of current child
        /// </summary>
        public Node Parent { get; protected set; }
    }
}