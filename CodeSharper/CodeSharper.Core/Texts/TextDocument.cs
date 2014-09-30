using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using CodeSharper.Core.Common.ConstraintChecking;

namespace CodeSharper.Core.Texts
{
    public class TextDocument
    {
        private readonly List<TextRange> _children;

        public TextRange TextRange { get; protected set; }

        public String Text { get; protected set; }

        public TextDocument(String text)
        {
            Constraints.NotNull(() => text);

            Text = text;
            TextRange = new TextRange(Text, this);
            _children = new List<TextRange>();
        }

        public IEnumerable<TextRange> Children
        {
            get { return _children; }
        }

        public TextDocument AppendChild(TextRange child)
        {
            Constraints
                .NotNull(() => child);

            _children.Add(child);

            return this;
        }

        public TextDocument RemoveChild(TextRange child)
        {
            _children.Remove(child);
            return this;
        }

        public TextRange SubStringOfText(Int32 start, Int32 exclusiveEnd)
        {
            var node = new TextRange(start, Text.Substring(start, exclusiveEnd - start), this);
            AppendChild(node);
            return node;
        }

        public TextRange SubStringOfText(Int32 start)
        {
            return SubStringOfText(start, Text.Length);
        }

        public TextDocument UpdateText(TextRange range, String value)
        {
            Constraints
                .NotNull(() => range)
                .NotNull(() => value);

            var start = range.Start;
            var length = range.Length;
            var offset = value.Length - length;

            Text = Text.Remove(start, length)
                       .Insert(start, value);

            if (offset != 0)
            {
                foreach (var child in Children.Where(child => child.Start > start))
                {
                    child.OffsetBy(offset);
                }
            }

            return this;
        }

        public TextRange AsTextNode()
        {
            return TextRange;
        }
    }
}