using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;

namespace CodeSharper.Core.Texts
{
    public class PreprocessedTextDocument : TextDocument
    {
        private readonly List<TextRange> _registeredTextRangesOfLines;

        public PreprocessedTextDocument(String text)
            : base(text)
        {
            _registeredTextRangesOfLines = _DetermineLinesOfText();
        }

        private List<TextRange> _DetermineLinesOfText()
        {
            var list = new List<TextRange>();

            foreach (var range in list)
                Unregister(range);
            list.Clear();

            var text = Text.ToString();
            var separator = Environment.NewLine;
            Int32 line = -1, index = -separator.Length, start = 0;

            while ((index = text.IndexOf(separator, index + separator.Length, StringComparison.Ordinal)) != -1)
            {
                _RegisterTextRangeOfLines(
                    new TextRange(start, index, this)
                        .OffsetBy(line == -1 ? 0 : separator.Length)
                );
                ++line;
                start = index;
            }

            if (index == -1)
                _RegisterTextRangeOfLines(new TextRange(start, text.Length, this).OffsetBy(separator.Length));

            return list;
        }

        private PreprocessedTextDocument _RegisterTextRangeOfLines(TextRange textRange)
        {
            Register(textRange);
            _registeredTextRangesOfLines.Add(textRange);
            return this;
        }

        public IEnumerable<TextRange> GetTextRangesByLines()
        {
            return _registeredTextRangesOfLines;
        }
    }
}