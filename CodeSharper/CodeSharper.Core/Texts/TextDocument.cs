using System;
using System.Collections.Generic;
using CodeSharper.Core.Common.ConstraintChecking;

namespace CodeSharper.Core.Texts
{
    public class TextDocument
    {
        private readonly List<TextNode> _children;

        public String Text { get; protected set; }

        public TextDocument(String text)
        {
            Constraints.NotNull(() => text);

            Text = text;

            _children = new List<TextNode>();
        }
        public IEnumerable<TextNode> Children
        {
            get { return _children; }
        }

        public TextDocument AppendChild(TextNode child)
        {
            Constraints
                .NotNull(() => child);

            _children.Add(child);

            return this;
        }

        public TextDocument RemoveChild(TextNode child)
        {
            _children.Remove(child);

            return this;
        }

        public TextNode GetSubText(Int32 start, Int32 exclusiveEnd)
        {
            var node = new TextNode(start, Text.Substring(start, exclusiveEnd - start), this);
            AppendChild(node);
            return node;
        }

    }
}