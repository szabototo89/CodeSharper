using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Common;
using CodeSharper.Core.Common.ConstraintChecking;

namespace CodeSharper.Core.Texts
{
    public class TextNode
    {
        private readonly List<TextNode> _children;

        public string Text { get; set; }

        public TextSpan TextSpan { get; set; }

        public IEnumerable<TextNode> Children
        {
            get { return _children; }
        }

        public TextNode Parent { get; protected set; }

        /// <summary>
        /// Constructor for cloning of TextNode
        /// </summary>
        private TextNode()
        {
            _children = new List<TextNode>();
        }

        public TextNode(String text, TextNode parent = null)
            : this(0, text, parent) { }

        public TextNode(Int32 start, String text, TextNode parent = null)
            : this()
        {
            Constraints.NotNull(() => text);

            Text = text;
            Parent = parent;

            TextSpan = new TextSpan(start, text);
            if (parent != null)
                parent._children.Add(this);
        }

        public TextNode GetSubText(Int32 start, Int32 exclusiveEnd)
        {
            var node = new TextNode(TextSpan.Start + start, Text.Substring(start, exclusiveEnd - start), this);
            _children.Add(node);
            return node;
        }

        public TextNode Detach()
        {
            if (Parent != null)
                Parent._children.Remove(this);

            Parent = null;

            return this;
        }

        public TextNode AttachTo(TextNode parent)
        {
            Constraints
                .NotNull(() => parent);

            Parent = parent;
            Parent._children.Add(this);

            return this;
        }
    }
}
