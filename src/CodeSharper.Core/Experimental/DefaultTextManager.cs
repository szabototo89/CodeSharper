using System;
using System.Collections.Generic;
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

        private readonly SortedList<TextPosition, TextSpan> textSpans;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultTextManager"/> class.
        /// </summary>
        public DefaultTextManager(String text)
        {
            Assume.NotNull(text, "text");

            textBuilder = new StringBuilder(text);
            lines = calculateLines(text);
            textSpans = new SortedList<TextPosition, TextSpan>();
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
            if (spanLines.Count == 0) return String.Empty;

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
            updateText(value, span);

            // update lines if value
            var valueLines = calculateLines(value);

            var startLine = span.Start.Line;
            var startColumn = span.Start.Column;
            var stopLine = span.Stop.Line;
            var stopColumn = span.Stop.Column;

            // update current text span
            span.Stop = new TextPosition(valueLines.Count - 1, valueLines[valueLines.Count - 1].Length);

            if (startLine != stopLine)
            {
                valueLines[0] = lines[startLine].Substring(0, startColumn) + valueLines[0];
                valueLines[valueLines.Count - 1] = valueLines[valueLines.Count - 1] + lines[stopLine].Substring(stopColumn + 1);

                lines.RemoveRange(startLine, stopLine - startLine + 1);
                lines.InsertRange(startLine, valueLines);

                /*// remove inner lines from original array
                if (stopLine - startLine > 1)
                {
                    lines.RemoveRange(startLine + 1, stopLine - startLine - 1);
                }

                if (startColumn == 0)
                {
                    // if start column is zero then it is not necessary to update it
                    // it needs to remove
                    lines.RemoveAt(startLine);
                }
                else
                {
                    // update the first line of span
                    lines[startLine] = lines[startLine].Remove(startColumn)
                                                       .Insert(startColumn, valueLines[0]);
                }

                // insert new lines to original
                if (valueLines.Count > 1)
                {
                    lines.AddRange(valueLines.Slice(1, valueLines.Count - 1));
                }

                // update the last line of span
                var lastLine = startLine + valueLines.Count - 1;
                lines[lastLine] = lines[lastLine].Remove(0, stopColumn + 1)
                                                 .Insert(0, valueLines[valueLines.Count - 1]);*/
            }
            else
            {
                // update current line of span
                lines[startLine] = lines[startLine].Remove(startColumn, stopColumn - startColumn)
                                                   .Insert(startColumn, value);
            }
        }

        private void updateText(String value, TextSpan span)
        {
            var startIndex = calculateIndex(span.Start);
            var stopIndex = calculateIndex(span.Stop);

            textBuilder.Remove(startIndex, stopIndex - startIndex + 1)
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
        public TextSpan CreateOrGetTextSpan(TextPosition inclusiveStart, TextPosition inclusiveStop)
        {
            var existingTextSpan = textSpans.Values.FirstOrDefault(span => span.Start.Equals(inclusiveStart) && span.Stop.Equals(inclusiveStop));
            if (existingTextSpan != null)
                return existingTextSpan;

            var textSpan = new TextSpan(inclusiveStart, inclusiveStop);
            textSpans.Add(inclusiveStart, textSpan);
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
            return textBuilder.ToString();
        }
    }
}