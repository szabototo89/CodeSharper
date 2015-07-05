using System;
using System.Collections.Generic;
using CodeSharper.Core.ErrorHandling;

namespace CodeSharper.Core.Texts
{
    public class TextRange : IEquatable<TextRange>, ICloneable
    {
        /// <summary>
        /// Gets or sets the start.
        /// </summary>
        public Int32 Start { get; protected internal set; }

        /// <summary>
        /// Gets or sets the stop.
        /// </summary>
        public Int32 Stop { get; protected internal set; }

        /// <summary>
        /// Gets the length.
        /// </summary>
        public Int32 Length
        {
            get { return Stop - Start; }
        }

        /// <summary>
        /// Gets or sets the text document.
        /// </summary>
        public TextDocument TextDocument { get; protected internal set; }

        /// <summary>
        /// Offsets the by.
        /// </summary>
        public void OffsetBy(Int32 offset)
        {
            Start += offset;
            Stop += offset;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextRange"/> class.
        /// </summary>
        public TextRange(Int32 start, Int32 stop) : this(start, stop, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextRange"/> class.
        /// </summary>
        public TextRange(Int32 start, Int32 stop, TextDocument textDocument)
        {
            Start = start;
            Stop = stop;
            TextDocument = textDocument;
        }

        #region Equality members

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public Boolean Equals(TextRange other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Start == other.Start && Stop == other.Stop && Equals(TextDocument, other.TextDocument);
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified object  is equal to the current object; otherwise, false.
        /// </returns>
        /// <param name="obj">The object to compare with the current object. </param>
        public override Boolean Equals(Object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TextRange) obj);
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        public override Int32 GetHashCode()
        {
            unchecked
            {
                var hashCode = Start;
                hashCode = (hashCode * 397) ^ Stop;
                hashCode = (hashCode * 397) ^ (TextDocument != null ? TextDocument.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion

        #region Cloning and copying

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public Object Clone()
        {
            return Copy();
        }

        public TextRange Copy(Int32? start = null, Int32? stop = null, TextDocument textDocument = null)
        {
            return new TextRange(
                start ?? Start,
                stop ?? Stop,
                textDocument ?? TextDocument);
        }

        #endregion

        #region Position comparer

        /// <summary>
        /// The position comparer
        /// </summary>
        public static readonly IComparer<TextRange> PositionComparer = new TextRangePositionComparer();

        private class TextRangePositionComparer : IComparer<TextRange>
        {
            /// <summary>
            /// Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.
            /// </summary>
            /// <param name="x">The first object to compare.</param>
            /// <param name="y">The second object to compare.</param>
            /// <returns>
            /// A signed integer that indicates the relative values of <paramref name="x" /> and <paramref name="y" />, as shown in the following table.Value Meaning Less than zero<paramref name="x" /> is less than <paramref name="y" />.Zero<paramref name="x" /> equals <paramref name="y" />.Greater than zero<paramref name="x" /> is greater than <paramref name="y" />.
            /// </returns>
            public Int32 Compare(TextRange x, TextRange y)
            {
                if (ReferenceEquals(x, y))
                    return 0;
                if (x == null)
                    return 1;
                if (y == null)
                    return -1;

                var startComparer = x.Start.CompareTo(y.Start);
                var stopComparer = x.Stop.CompareTo(y.Stop);

                if (startComparer < 0 && stopComparer > 0)
                    return 1;
                if (startComparer > 0 && stopComparer < 0)
                    return -1;
                if (startComparer != 0)
                    return startComparer;

                return stopComparer;
            }
        }

        #endregion

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override String ToString()
        {
            return String.Format("TextRange({0}, {1})", Start, Stop);
        }
    }
}