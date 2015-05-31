using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using CodeSharper.Core.Nodes.Selectors;
using CodeSharper.Core.Texts;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Languages.Text.Nodes.Selectors
{
    public abstract class TextSelectorBase : SelectorBase
    {
        /// <summary>
        /// Filters the specified element. Returns true if specified element is in the selection otherwise false.
        /// </summary>
        public override IEnumerable<Object> SelectElement(Object element)
        {
            var textRange = element as TextRange;
            if (textRange == null)
                return Enumerable.Empty<Object>();

            return SelectElement(textRange);
        }

        public abstract IEnumerable<TextRange> SelectElement(TextRange textRange);
    }

    public class CharacterSelector : TextSelectorBase
    {
        /// <summary>
        /// Filters the specified element. Returns true if specified element is in the selection otherwise false.
        /// </summary>
        public override IEnumerable<TextRange> SelectElement(TextRange textRange)
        {
            var textDocument = textRange.TextDocument;
            var text = textRange.GetText();

            for (var i = 0; i < text.Length; i++)
            {
                var start = textRange.Start + i;
                yield return textDocument.CreateOrGetTextRange(start, start + 1);
            }
        }
    }

    public class WordSelector : TextSelectorBase
    {
        private readonly Char[] separators = { ' ', '.', ',', '!', '?', '\n', '\r', '\t' };

        /// <summary>
        /// Selects the element.
        /// </summary>
        public override IEnumerable<TextRange> SelectElement(TextRange textRange)
        {
            var textDocument = textRange.TextDocument;
            var text = textRange.GetText();
            var index = 0;
            var firstWhitespacePosition = 0;

            do
            {
                firstWhitespacePosition = text.IndexOfAny(separators, index);
                if (firstWhitespacePosition == -1) break;
                var result = text.Substring(index, firstWhitespacePosition - index);
                if (!String.IsNullOrWhiteSpace(result))
                    yield return textDocument.CreateOrGetTextRange(textRange.Start + index, textRange.Start + firstWhitespacePosition);
                index = firstWhitespacePosition + 1;
            } while (firstWhitespacePosition != -1);

            yield return textDocument.CreateOrGetTextRange(textRange.Start + index, textRange.Start + text.Length);
        }
    }

    public abstract class TextSeparatorSelector : TextSelectorBase
    {
        private readonly Regex _regex;

        /// <summary>
        /// Gets the pattern.
        /// </summary>
        protected abstract String Pattern { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextSeparatorSelector"/> class.
        /// </summary>
        protected TextSeparatorSelector()
        {
            _regex = new Regex(Pattern);
        }

        /// <summary>
        /// Selects the element.
        /// </summary>
        public override IEnumerable<TextRange> SelectElement(TextRange textRange)
        {
            var textDocument = textRange.TextDocument;
            var text = textRange.GetText();
            var index = 0;

            var match = _regex.Match(text, index);
            while (match != Match.Empty)
            {
                yield return textDocument.CreateOrGetTextRange(textRange.Start + index, textRange.Start + match.Index);
                index = match.Index + match.Length;
                match = _regex.Match(text, index);
            }

            yield return textDocument.CreateOrGetTextRange(textRange.Start + index, textRange.Stop);
        }
    }

    public class ParagraphSelector : TextSeparatorSelector
    {
        /// <summary>
        /// Gets the pattern.
        /// </summary>
        protected override String Pattern
        {
            get { return "(\r?\n){2,}"; }
        }
    }

    public class LineSelector : TextSeparatorSelector
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is empty line included.
        /// </summary>
        public Boolean IsEmptyLineIncluded { get; set; }

        /// <summary>
        /// Gets the pattern.
        /// </summary>
        protected override String Pattern
        {
            get { return "\r?\n"; }
        }

        /// <summary>
        /// Selects the element.
        /// </summary>
        public override IEnumerable<TextRange> SelectElement(TextRange textRange)
        {
            return base.SelectElement(textRange).Where(line => {
                var text = line.GetText();
                return IsEmptyLineIncluded || !String.IsNullOrWhiteSpace(text);
            });
        }
    }
}