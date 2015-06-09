using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using CodeSharper.Core.Texts;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Languages.Text.Nodes.Selectors
{
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
}