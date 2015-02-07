using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeSharper.Core.ErrorHandling;

namespace CodeSharper.Core.Texts
{
    public class TextDocument : ITextDocument
    {
        private readonly List<TextRange> _textRanges;

        /// <summary>
        /// Gets or sets the text of document.
        /// </summary>
        public StringBuilder Text { get; protected set; }

        /// <summary>
        /// Gets or sets the text range of document
        /// </summary>
        public TextRange TextRange { get; protected set; }

        /// <summary>
        /// Gets or sets the text ranges.
        /// </summary>
        public IEnumerable<TextRange> TextRanges
        {
            get { return _textRanges; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextDocument"/> class.
        /// </summary>
        public TextDocument(String text)
        {
            Assume.NotNull("text", text);

            _textRanges = new List<TextRange>();

            Text = new StringBuilder(text);
            TextRange = createTextRange(0, Text.Length);
        }

        /// <summary>
        /// Registers the specified text range of TextDocument. 
        /// </summary>
        public void Register(TextRange textRange)
        {
            Assume.NotNull(textRange, "textRange");
            _textRanges.Add(textRange);
        }

        /// <summary>
        /// Unregisters the specified text range in TextDocument
        /// </summary>
        public void Unregister(TextRange textRange)
        {
            Assume.NotNull(textRange, "textRange");
            _textRanges.Remove(textRange);
        }

        /// <summary>
        /// Gets an existing or creates a new text range in text document
        /// </summary>
        public TextRange GetOrCreateTextRange(Int32 start, Int32 stop)
        {
            Assume.IsTrue(start <= stop, "start must be smaller than stop!");
            Assume.IsTrue(start >= 0, "start must be a positive or zero!");
            Assume.IsTrue(stop - start <= Text.Length, "stop cannot be larger than text length!");

            var textRange = getExistingTextRange(start, stop)?? 
                            createTextRange(start, stop);

            return textRange;
        }

        /// <summary>
        /// Updates the text of text document based on a specified text range
        /// </summary>
        public TextDocument UpdateText(Int32 oldStart, Int32 oldStop, TextRange updatedTextRange)
        {
            Assume.NotNull(updatedTextRange, "updatedTextRange");

            Text.Remove(oldStart, oldStop)
                .Insert(updatedTextRange.Start, updatedTextRange.Text);

            return this;
        }

        #region Helper methods for creating text ranges

        private TextRange createTextRange(Int32 start, Int32 stop)
        {
            var textRange = new TextRange(start, stop, this);
            Register(textRange);
            return textRange;
        }

        private TextRange getExistingTextRange(Int32 start, Int32 stop)
        {
            return _textRanges.FirstOrDefault(textRange => textRange.Start == start && textRange.Stop == stop);
        }

        #endregion
    }
}
