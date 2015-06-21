using System;
using System.Collections.Generic;
using CodeSharper.Core.Texts;

namespace CodeSharper.Core.Experimental
{
    public class TextSpan : TextSpan<TextPosition>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextSpan"/> class.
        /// </summary>
        public TextSpan(TextPosition start, TextPosition stop) : base(start, stop)
        {
        }
    }

    public class TextSpan<TPosition> : IEquatable<TextSpan<TPosition>>
        where TPosition : IComparable<TPosition>
    {
        /// <summary>
        /// Gets or sets the start.
        /// </summary>
        public TPosition Start { get; internal protected set; }

        /// <summary>
        /// Gets or sets the stop.
        /// </summary>
        public TPosition Stop { get; internal protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextSpan"/> class.
        /// </summary>
        public TextSpan(TPosition start, TPosition stop)
        {
            if (start.CompareTo(stop) > 0)
                throw new ArgumentException("Start should be lesser than stop.");

            Start = start;
            Stop = stop;
        }

        #region Position comparer

        /// <summary>
        /// The position comparer
        /// </summary>
        public static readonly IComparer<TextSpan> PositionComparer = new TextRangePositionComparer();

        private class TextRangePositionComparer : IComparer<TextSpan>
        {
            /// <summary>
            /// Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.
            /// </summary>
            /// <param name="x">The first object to compare.</param>
            /// <param name="y">The second object to compare.</param>
            /// <returns>
            /// A signed integer that indicates the relative values of <paramref name="x" /> and <paramref name="y" />, as shown in the following table.Value Meaning Less than zero<paramref name="x" /> is less than <paramref name="y" />.Zero<paramref name="x" /> equals <paramref name="y" />.Greater than zero<paramref name="x" /> is greater than <paramref name="y" />.
            /// </returns>
            public Int32 Compare(TextSpan x, TextSpan y)
            {
                if (ReferenceEquals(x, y)) return 0;
                if (x == null) return 1;
                if (y == null) return -1;

                var startComparer = x.Start.CompareTo(y.Start);
                var stopComparer = x.Stop.CompareTo(y.Stop);

                if (startComparer < 0 && stopComparer > 0) return 1;
                if (startComparer > 0 && stopComparer < 0) return -1;
                if (startComparer != 0) return startComparer;

                return stopComparer;
            }
        }

        #endregion

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public Boolean Equals(TextSpan<TPosition> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Start.Equals(other.Start) && Stop.Equals(other.Stop);
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
            return Equals((TextSpan<TPosition>) obj);
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
                return (Start.GetHashCode() * 397) ^ Stop.GetHashCode();
            }
        }

        public Boolean IsOverlapping(TextSpan<TPosition> other)
        {
            if (this.Equals(other)) return true;

            return (Start.CompareTo(other.Start) >= 0 &&
                    Stop.CompareTo(other.Stop) <= 0) ||
                   (other.Start.CompareTo(Start) >= 0 &&
                    other.Stop.CompareTo(Stop) <= 0)
                ;
        }

        public Boolean IsSubRangeOf(TextSpan<TPosition> other)
        {
            if (this.Equals(other)) return true;

            return other.Start.CompareTo(Start) <= 0 &&
                   other.Stop.CompareTo(Stop) >= 0;
        }

        public Boolean IsSuperRangeOf(TextSpan<TPosition> other)
        {
            if (this.Equals(other)) return true;

            return Start.CompareTo(other.Start) <= 0 &&
                   Stop.CompareTo(other.Stop) >= 0;
        }
    }
}