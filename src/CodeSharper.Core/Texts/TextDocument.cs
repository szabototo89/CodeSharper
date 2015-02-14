using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using CodeSharper.Core.Common;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Core.Texts
{
    public class TextDocument : ITextDocument
    {
        private StringBuilder _text;

        /// <summary>
        /// Gets or sets the text of document.
        /// </summary>
        public String Text
        {
            get { return _text.ToString(); }
            protected set { _text = new StringBuilder(value); }
        }

        /// <summary>
        /// Gets or sets the text range of document
        /// </summary>
        public TextRange TextRange { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextDocument"/> class.
        /// </summary>
        public TextDocument(String text)
        {
            Assume.NotNull("text", text);

            Text = text;
            TextRange = CreateOrGetTextRange(0, text.Length);
        }

        /// <summary>
        /// Gets the text by specified text range
        /// </summary>
        public String GetText(TextRange textRange)
        {
            Assume.NotNull(textRange, "updatableTextRange");
            return _text.ToString(textRange.Start, textRange.Stop - textRange.Start);
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

            var textRange = getExistingTextRange(start, stop);
            var isFoundExistingTextRange = (textRange != null);

            if (!isFoundExistingTextRange)
                textRange = createTextRange(start, stop);

            return textRange;
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

            foreach (var range in TextRange.AsEnumerable()
                                           .Where(range => !ReferenceEquals(range, updatableTextRange)))
            {
                if (isNoConflictWith(range, updatableTextRange))
                {
                    range.OffsetBy(offset);
                }
                else if (isSubRangeOf(range, updatableTextRange))
                {
                    throw new NotImplementedException();
                }
                else if (isSuperRangeOf(range, updatableTextRange))
                {
                    range.Stop += offset;
                }
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

            _text.Remove(textRange.Start, textRange.Stop - textRange.Start)
                 .Insert(textRange.Start, replacedText);

            return this;
        }

        #region Helper methods for creating or getting back text ranges

        private TextRange createTextRange(Int32 start, Int32 stop)
        {
            var current = TextRange;

            while (current != null)
            {
                if (current.Start > start)
                    return insertTextRangeBeforeTo(current, start, stop);

                if (current.Start == start && current.Stop > stop)
                    return insertTextRangeBeforeTo(current, start, stop);

                if (current.Next == null)
                    return insertTextRangeAfterTo(current, start, stop);

                current = current.Next;
            }

            if (TextRange == null)
                return initializeTextRangeRoot(start, stop);

            throw new NotImplementedException();
        }

        private TextRange initializeTextRangeRoot(Int32 start, Int32 stop)
        {
            return new TextRange(start, stop, this);
        }

        private TextRange insertTextRangeBeforeTo(TextRange current, Int32 start, Int32 stop)
        {
            var textRange = new TextRange(start, stop, this,
                previous: current.Previous,
                next: current);

            if (current.Previous != null)
                current.Previous.Next = textRange;

            current.Previous = textRange;

            if (ReferenceEquals(current, TextRange))
                TextRange = textRange;

            return textRange;
        }

        private TextRange insertTextRangeAfterTo(TextRange current, Int32 start, Int32 stop)
        {
            var textRange = new TextRange(start, stop, this,
                previous: current,
                next: current.Next);

            if (current.Next != null)
            {
                current.Next.Previous = textRange;
            }

            if (current.Previous != null)
            {
                current.Previous.Next = textRange;
            }

            current.Next = textRange;

            return textRange;
        }

        private TextRange getExistingTextRange(Int32 start, Int32 stop)
        {
            var current = TextRange;

            while (current != null)
            {
                if (current.Start == start && current.Stop == stop)
                    break;

                current = current.Next;
            }

            return current;
        }

        #endregion

        #region Helper methods for changing text ranges in TextDocument

        private void changeToUpdatedTextRange(TextRange oldTextRange, TextRange newTextRange)
        {
            if (ReferenceEquals(oldTextRange, TextRange))
                TextRange = newTextRange;

            if (oldTextRange.Previous != null)
                oldTextRange.Previous.Next = newTextRange;

            if (oldTextRange.Next != null)
                oldTextRange.Next.Previous = newTextRange;
        }

        #endregion
    }
}
