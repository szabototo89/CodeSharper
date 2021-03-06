﻿using System;
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
        /// Gets the next element of Object
        /// </summary>
        public TextRange Next { get; protected internal set; }

        /// <summary>
        /// Gets the previous element of Object
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
        /// <param name="obj">The Object to compare with the current Object.</param>
        /// <returns>
        /// true if the specified Object  is equal to the current Object; otherwise, false.
        /// </returns>
        public override Boolean Equals(Object obj)
        {
            return Equals(obj as TextRange);
        }

        /// <summary>
        /// Indicates whether the current Object is equal to another Object of the same type.
        /// </summary>
        /// <param name="other">An Object to compare with this Object.</param>
        /// <returns>
        /// true if the current Object is equal to the <paramref name="other" /> parameter; otherwise, false.
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
        /// Returns a String that represents the current Object.
        /// </summary>
        /// <returns>
        /// A String that represents the current Object.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override String ToString()
        {
            return String.Format("TextRange({0}:{1})", Start, Stop);
        }
    }
}

