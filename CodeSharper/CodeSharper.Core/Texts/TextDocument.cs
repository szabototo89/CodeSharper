using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using CodeSharper.Core.Common.ConstraintChecking;

namespace CodeSharper.Core.Texts
{
    public class TextDocument
    {
        private readonly List<TextRange> _registeredTextRanges;  

        public String Text { get; private set; }

        public TextRange TextRange { get; private set; }

        public TextDocument(String text)
        {
            Constraints.NotNull(() => text);

            _registeredTextRanges = new List<TextRange>();
            Text = text;
            TextRange = new TextRange(0, Text.Length, this);
        }

        public TextDocument Register(TextRange textRange)
        {
            Constraints.NotNull(() => textRange);
            _registeredTextRanges.Add(textRange);
            return this;
        }

        public TextDocument Unregister(TextRange textRange)
        {
            Constraints.NotNull(() => textRange);
            _registeredTextRanges.Remove(textRange);
            return this;
        }

        public TextDocument ReplaceText(TextRange updatedTextRange, String newValue)
        {
            Constraints
                .NotNull(() => updatedTextRange)
                .NotNull(() => newValue);

            var offsetLength = newValue.Length - updatedTextRange.Length;

            Text = Text
                .Remove(updatedTextRange.Start, updatedTextRange.Length)
                .Insert(updatedTextRange.Start, newValue);

            foreach (var range in _registeredTextRanges
                .Where(r => r.Start > updatedTextRange.Start))
            {
                range.OffsetBy(offsetLength);
            }

            return this;
        }
    }
}