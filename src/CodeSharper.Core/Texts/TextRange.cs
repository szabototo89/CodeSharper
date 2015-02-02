using System.Collections.Generic;

namespace CodeSharper.Core.Texts
{
    using System;
    using ErrorHandling;

    public class TextRange : IEquatable<TextRange>, IHasChildren<TextRange>, IDisposable
    {
        private readonly List<TextRange> _children;

        /// <summary>
        /// Gets or sets start position of text range
        /// </summary>
        public Int32 Start { get; protected set; }

        /// <summary>
        /// Gets or sets stop position of text range
        /// </summary>
        public Int32 Stop { get; protected set; }

        /// <summary>
        /// Gets value of text range
        /// </summary>
        public String Text { get; protected set; }

        /// <summary>
        /// Gets or sets the length of text
        /// </summary>
        public Int32 Length { get; protected set; }

        /// <summary>
        /// Gets or sets the text document of text range
        /// </summary>
        public ITextDocument TextDocument { get; protected set; }

        /// <summary>
        /// Gets or sets children of text range
        /// </summary>
        public IEnumerable<TextRange> Children
        {
            get { return _children.AsReadOnly(); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextRange" /> class.
        /// </summary>
        /// <param name="start">Start of TextRange.</param>
        /// <param name="stop">Stop of TextRange.</param>
        /// <param name="textDocument">Text document reference of TextRange.</param>
        internal TextRange(Int32 start, Int32 stop, ITextDocument textDocument)
        {
            Assume.IsTrue(start <= stop, "Start must be less than stop!");
            Assume.IsTrue(start >= 0, "Start must be positive or zero!");
            Assume.NotNull(textDocument, "textDocument");

            Start = start;
            Stop = stop;
            Length = stop - start;
            _children = new List<TextRange>();

            TextDocument = textDocument;
            Text = CreateTextFromTextDocument(start, stop, TextDocument);
        }

        private string CreateTextFromTextDocument(Int32 start, Int32 stop, ITextDocument textDocument)
        {
            Assume.IsTrue(start <= stop, "Start must be less than stop!");
            Assume.NotNull(textDocument, "textDocument");

            return TextDocument.Text.ToString(start, Length);
        }

        #region Equality members of TextRange

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public Boolean Equals(TextRange other)
        {
            if (ReferenceEquals(other, null)) return false;
            if (ReferenceEquals(other, this)) return true;

            return ReferenceEquals(TextDocument, other.TextDocument) &&
                   Start.Equals(other.Start) &&
                   Stop.Equals(other.Stop);
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified object  is equal to the current object; otherwise, false.
        /// </returns>
        /// <param name="obj">The object to compare with the current object. </param><filterpriority>2</filterpriority>
        public override Boolean Equals(Object obj)
        {
            return this.Equals(obj as TextRange);
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override Int32 GetHashCode()
        {
            unchecked
            {
                return (Start * 397) ^ (Text != null ? Text.GetHashCode() : 0);
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            TextDocument.Unregister(this);
        }

        #endregion

        /// <summary>
        /// Returns sub range of the object based on start and stop positions.
        /// </summary>
        public TextRange SubRange(Int32 start, Int32 stop, Boolean areRelativePositions = true)
        {
            Assume.IsTrue(start <= stop, "start must be smaller than stop!");

            TextRange subRange = areRelativePositions
                                    ? SubRangeWithRelativePositions(start, stop)
                                    : SubRangeWithAbsolutePositions(start, stop);

            AddSubRangeIntoChildren(subRange);

            return subRange;
        }

        private void AddSubRangeIntoChildren(TextRange subRange)
        {
            _children.Add(subRange);
        }

        #region Helper methods for creating subrange of text range

        private TextRange SubRangeWithAbsolutePositions(Int32 start, Int32 stop)
        {
            Assume.IsTrue(Start <= start, "start is out of range!");
            Assume.IsTrue(stop <= Stop, "stop is out of range!");

            return new TextRange(start, stop, TextDocument);
        }

        private TextRange SubRangeWithRelativePositions(Int32 start, Int32 stop)
        {
            Assume.IsTrue(start >= 0, "start must be positive");
            Assume.IsTrue(stop < Length, "stop cannot be greater than length of text range");

            return new TextRange(Start + start, Start + stop, TextDocument);
        }

        #endregion

    }
}

