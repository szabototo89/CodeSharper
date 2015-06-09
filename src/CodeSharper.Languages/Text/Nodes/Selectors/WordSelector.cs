using System;
using System.Collections.Generic;
using CodeSharper.Core.Texts;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Languages.Text.Nodes.Selectors
{
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
}