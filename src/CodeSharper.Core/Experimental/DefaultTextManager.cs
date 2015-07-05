using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mime;
using System.Text;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.Texts;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Core.Experimental
{
    public class DefaultTextManager : ITextManager
    {
        private readonly StringBuilder textBuilder;
        private List<String> lines;

        private readonly SortedList<TextSpan, TextSpan> textSpans;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultTextManager"/> class.
        /// </summary>
        public DefaultTextManager(String text)
        {
            Assume.NotNull(text, "text");

            textBuilder = new StringBuilder(text);
            lines = calculateLines(text);
            textSpans = new SortedList<TextSpan, TextSpan>(TextSpan.PositionComparer);
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public String GetValue(TextSpan span)
        {
            Assume.NotNull(span, "span");
            if (lines.Count <= span.Start.Line || lines.Count <= span.Stop.Line)
                throw new IndexOutOfRangeException();

            // retrieve lines of text
            var spanLines = lines.Slice(span.Start.Line, span.Stop.Line + 1);
            if (spanLines.Count == 0)
                return String.Empty;

            if (spanLines.Count == 1)
                return spanLines[0].Substring(span.Start.Column, span.Stop.Column - span.Start.Column);

            // calculate first line of span
            spanLines[0] = spanLines[0].Substring(span.Start.Column);

            // calculate last line of span
            spanLines[spanLines.Count - 1] = spanLines[spanLines.Count - 1].Substring(0, span.Stop.Column);

            // return whole text span
            return String.Join(Environment.NewLine, spanLines);
        }

        /// <summary>
        /// Sets the value.
        /// </summary>
        public void SetValue(String value, TextSpan span)
        {
            Assume.NotNull(value, "value");
            Assume.NotNull(span, "span");

            // update index
            // updateText(span, value);
            var valueLines = calculateLines(value);

            // update other spans
            updateTextSpans(span, valueLines);

            // update lines if value
            updateLines(span, valueLines);
        }

        private void updateTextSpans(TextSpan span, List<String> valueLines)
        {
            var indexOfCurrentTextSpan = textSpans.IndexOfValue(span);
            if (indexOfCurrentTextSpan == -1)
                throw new InvalidProgramException(String.Format("Invalid text span: {0}", span));

            // update current text span
            var updatedStopLine = span.Start.Line + valueLines.Count - 1;
            var updatedStopColumn = span.Start.Column + valueLines[valueLines.Count - 1].Length;

            var offsetColumn = updatedStopColumn - span.Stop.Column;
            var offsetLine = updatedStopLine - span.Stop.Line;

            // update text spans in current line
            if (offsetColumn != 0)
            {
                for (var i = indexOfCurrentTextSpan + 1; i < textSpans.Count; i++)
                {
                    var textSpan = textSpans.Values[i];

                    if (textSpan.Start.Line == span.Stop.Line && textSpan.Start.CompareTo(span.Stop) >= 0)
                        break;

                    textSpan.Start = textSpan.Start.Offset(0, offsetColumn);
                    if (textSpan.Start.Line == textSpan.Stop.Line)
                        textSpan.Stop = textSpan.Stop.Offset(0, offsetColumn);
                }
            }

            // more than one line has been added
            // we need to update other text ranges as well
            if (offsetLine != 0)
            {
                var updatableTextSpans = textSpans.Values.Skip(indexOfCurrentTextSpan + 1);

                foreach (var textSpan in updatableTextSpans)
                {
                    textSpan.Stop = textSpan.Stop.Offset(offsetLine, 0);
                    if (textSpan.Start.Line >= span.Stop.Line)
                        textSpan.Start = textSpan.Start.Offset(offsetLine, 0);
                }
            }
        }

        private void updateLines(TextSpan span, IList<String> valueLines)
        {
            var startLine = span.Start.Line;
            var startColumn = span.Start.Column;
            var stopLine = span.Stop.Line;
            var stopColumn = span.Stop.Column;

            // update current text span
            span.Stop = new TextPosition(span.Start.Line + valueLines.Count - 1,
                                         span.Start.Column + valueLines[valueLines.Count - 1].Length);

            valueLines[0] = lines[startLine].Substring(0, startColumn) + valueLines[0];
            valueLines[valueLines.Count - 1] = valueLines[valueLines.Count - 1] + lines[stopLine].Substring(stopColumn);

            lines.RemoveRange(startLine, stopLine - startLine + 1);
            lines.InsertRange(startLine, valueLines);
        }

        private void updateText(TextSpan span, String value)
        {
            var startIndex = calculateIndex(span.Start);
            var stopIndex = calculateIndex(span.Stop);

            textBuilder.Remove(startIndex, stopIndex - startIndex)
                       .Insert(startIndex, value);
        }

        private Int32 calculateIndex(TextPosition position)
        {
            var charactersCountInLines = lines.Take(position.Line)
                                              .Sum(line => line.Length + Environment.NewLine.Length);

            return charactersCountInLines + position.Column;
        }

        /// <summary>
        /// Creates the or get text span.
        /// </summary>
        public TextSpan CreateOrGetTextSpan(TextPosition inclusiveStart, TextPosition exclusiveStop)
        {
            var indexOfExistingTextSpan = textSpans.IndexOfKey(new TextSpan(inclusiveStart, exclusiveStop));
            if (indexOfExistingTextSpan != -1)
                return textSpans.Values[indexOfExistingTextSpan];

            var textSpan = new TextSpan(inclusiveStart, exclusiveStop);
            textSpans.Add(textSpan, textSpan);
            return textSpan;
        }

        private List<String> calculateLines(String text)
        {
            return text.Split(new[] {Environment.NewLine}, StringSplitOptions.None).ToList();
        }

        /// <summary>
        /// Gets the text.
        /// </summary>
        public String GetText()
        {
            return String.Join(Environment.NewLine, lines);
        }
    }
}