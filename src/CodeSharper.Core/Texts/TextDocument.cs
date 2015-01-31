using System;
using System.Collections.Generic;
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
            TextRange = new TextRange(0, text.Length, this);
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
    }
}
