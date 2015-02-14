using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using CodeSharper.Core.Common;
using CodeSharper.Core.ErrorHandling;

namespace CodeSharper.Core.Texts
{
    public class TextRange : ILinkedStyle<TextRange>, IEquatable<TextRange>
    {
        /// <summary>
        /// Gets or sets the start of text range
        /// </summary>
        public Int32 Start { get; protected internal set; }

        /// <summary>
        /// Gets or sets the stop of text range
        /// </summary>
        public Int32 Stop { get; protected internal set; }

        /// <summary>
        /// Gets or sets the text document of text range
        /// </summary>
        public TextDocument TextDocument { get; protected set; }

        /// <summary>
        /// Gets the next element of object
        /// </summary>
        public TextRange Next { get; protected internal set; }

        /// <summary>
        /// Gets the previous element of object
        /// </summary>
        public TextRange Previous { get; protected internal set; }

        /// <summary>
        /// Gets the length of text
        /// </summary>
        public Int32 Length { get { return Stop - Start; } }

        /// <summary>
        /// Offsets position (start and stop) by specified value
        /// </summary>
        internal void OffsetBy(Int32 offset)
        {
            Start += offset;
            Stop += offset;
        }

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TextRange"/> class.
        /// </summary>
        internal TextRange(Int32 start, Int32 stop, TextDocument textDocument, TextRange previous = null,
            TextRange next = null)
        {
            Assume.IsTrue(start >= 0, "start must be positive or zero!");
            Assume.IsTrue(start <= stop, "start must be lesser than stop!");
            Assume.NotNull(textDocument, "textDocument");

            Start = start;
            Stop = stop;
            TextDocument = textDocument;

            Previous = previous;
            Next = next;
        }

        #endregion

        #region Equality methods of TextRange

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>
        /// true if the specified object  is equal to the current object; otherwise, false.
        /// </returns>
        public override Boolean Equals(Object obj)
        {
            return Equals(obj as TextRange);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        public Boolean Equals(TextRange other)
        {
            if (ReferenceEquals(other, null)) return false;
            if (ReferenceEquals(other, this)) return true;

            return Start.Equals(other.Start) &&
                   Stop.Equals(other.Stop) &&
                   TextDocument.Equals(other.TextDocument);
        }

        /// <summary>
        /// Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object" />.
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

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override String ToString()
        {
            return String.Format("TextRange({0}:{1})", Start, Stop);
        }
    }
}

