using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using CodeSharper.Core.Common.ConstraintChecking;

namespace CodeSharper.Core.Texts
{
    public class TextDocument
    {
        private readonly List<TextNode> _children;

        public TextNode TextNode { get; protected set; }

        public String Text { get; protected set; }

        public TextDocument(String text)
        {
            Constraints.NotNull(() => text);

            Text = text;
            TextNode = new TextNode(Text, this);

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

        public TextNode SubStringOfText(Int32 start, Int32 exclusiveEnd)
        {
            var node = new TextNode(start, Text.Substring(start, exclusiveEnd - start), this);
            AppendChild(node);
            return node;
        }

        public TextNode SubStringOfText(Int32 start)
        {
            return SubStringOfText(start, Text.Length);
        }

        public TextDocument UpdateText(TextNode node, String value)
        {
            Constraints
                .NotNull(() => node)
                .NotNull(() => value);

            var start = node.TextSpan.Start;
            var length = node.TextSpan.Length;

            var offset = value.Length - length;

            Text = Text.Remove(start, length)
                       .Insert(start, value);

            if (offset != 0)
                foreach (var child in Children.Where(child => child.TextSpan.Start > start))
                    child.TextSpan = child.TextSpan.OffsetBy(offset);

            return this;
        }

        public TextNode AsTextNode()
        {
            return TextNode;
        }
    }
}