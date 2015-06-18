using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using CodeSharper.Core.Common;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.Texts;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Core.Texts
{
    public class TextDocument : ITextDocument
    {
        private StringBuilder text;
        private readonly SortedList<TextRange, TextRange> textRanges;

        /// <summary>
        /// Gets or sets the text of document.
        /// </summary>
        public String Text
        {
            get { return text.ToString(); }
            protected set { text = new StringBuilder(value); }
        }

        /// <summary>
        /// Gets or sets the text range of document
        /// </summary>
        public TextRange TextRange { get; protected set; }

        /// <summary>
        /// Gets or sets the text ranges.
        /// </summary>
        public IEnumerable<TextRange> TextRanges
        {
            get { return textRanges.Values.ToList().AsReadOnly(); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextDocument"/> class.
        /// </summary>
        public TextDocument(String text)
        {
            Assume.NotNull("text", text);

            Text = text;
            textRanges = new SortedList<TextRange, TextRange>(TextRange.PositionComparer);
            TextRange = CreateOrGetTextRange(0, text.Length);
        }

        /// <summary>
        /// Gets the text by specified text range
        /// </summary>
        public String GetText(TextRange textRange)
        {
            Assume.NotNull(textRange, "textRange");
            return text.ToString(textRange.Start, textRange.Stop - textRange.Start);
        }

        /// <summary>
        /// Removes the text.
        /// </summary>
        /// <param name="textRange">The text range.</param>
        /// <returns></returns>
        public TextDocument RemoveText(TextRange textRange)
        {
            Assume.NotNull(textRange, "textRange");

            ChangeText(textRange, String.Empty);
            removeTextRange(textRange);

            return this;
        }

        /// <summary>
        /// Creates a new or gets an existing text range from text document
        /// </summary>
        /// <param name="start">The start position of text range</param>
        /// <param name="stop">The stop position of text range</param>
        public TextRange CreateOrGetTextRange(Int32 start, Int32 stop)
        {
            Assume.IsTrue(start >= 0, "start must be positive or zero!");
            Assume.IsTrue(start <= stop, "start must be lesser than stop!");

            var indexOfTextRange = textRanges.IndexOfKey(new TextRange(start, stop));
            if (indexOfTextRange != -1)
                return textRanges.Values[indexOfTextRange];

            return createTextRange(start, stop);
        }

        /// <summary>
        /// Changes the text in text document. It removes passed text range and creates a new one. Finally, it returns the updated text range. 
        /// </summary>
        public TextDocument ChangeText(TextRange textRange, String replacedText)
        {
            Assume.NotNull(textRange, "textRange");
            Assume.NotNull(replacedText, "replacedText");

            ChangeRawText(textRange, replacedText);

            var offset = textRange.Start + replacedText.Length - textRange.Stop;

            updateTextRanges(textRange, offset);

            textRange.Stop = textRange.Start + replacedText.Length;

            return this;
        }

        private void updateTextRanges(TextRange updatableTextRange, Int32 offset)
        {
            Assume.NotNull(updatableTextRange, "updatableTextRange");
            if (offset == 0) return;

            var indexOfTextRange = textRanges.IndexOfValue(updatableTextRange);
            var removableTextRanges = new List<TextRange>();

            foreach (var range in textRanges.Values.Skip(indexOfTextRange + 1))
            {
                if (isNoConflictWith(range, updatableTextRange)) range.OffsetBy(offset);
                else if (isSubRangeOf(range, updatableTextRange))
                {
                    removableTextRanges.Add(range);
                    // throw new NotImplementedException(); 
                }
                else if (isSuperRangeOf(range, updatableTextRange)) range.Stop += offset;
                else if (isOverlapping(range, updatableTextRange))
                {
                    range.OffsetBy(offset);
                    var gap = updatableTextRange.Stop - range.Start + offset;

                    if (updatableTextRange.Length + offset < gap)
                    {
                        var difference = gap - (updatableTextRange.Length + offset);
                        range.Start = Math.Max(range.Start + difference, 0);
                    }
                }
            }

            foreach (var range in removableTextRanges)
            {
                textRanges.Remove(range);
            }
        }

        private Boolean isSubRangeOf(TextRange range, TextRange otherRange)
        {
            return isSuperRangeOf(otherRange, range);
        }

        private Boolean isOverlapping(TextRange range, TextRange otherRange)
        {
            return range.Start >= otherRange.Start && range.Start <= otherRange.Stop;
        }

        private Boolean isSuperRangeOf(TextRange range, TextRange otherRange)
        {
            return range.Start <= otherRange.Start && otherRange.Stop <= range.Stop;
        }

        private Boolean isNoConflictWith(TextRange range, TextRange otherRange)
        {
            return range.Start > otherRange.Stop;
        }

        /// <summary>
        /// Changes text in text document without updating positions of other text ranges.
        /// </summary>
        public TextDocument ChangeRawText(TextRange textRange, String replacedText)
        {
            Assume.NotNull(textRange, "updatableTextRange");
            Assume.NotNull(replacedText, "replacedText");

            text.Remove(textRange.Start, textRange.Stop - textRange.Start)
                .Insert(textRange.Start, replacedText);

            return this;
        }

        #region Helper methods for creating or getting back text ranges

        private TextRange createTextRange(Int32 start, Int32 stop)
        {
            var textRange = new TextRange(start, stop, this);
            textRanges.Add(textRange, textRange);

            return textRange;
        }

        #endregion

        #region Helper methods for removing text range in TextDocument

        private void removeTextRange(TextRange textRange)
        {
            textRanges.Remove(textRange);
        }

        #endregion
    }
}