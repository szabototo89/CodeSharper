using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Texts;

namespace CodeSharper.Core.Experimental
{
    public struct TextPosition : IComparable<TextPosition>, IEquatable<TextPosition>
    {
        /// <summary>
        /// Gets the line.
        /// </summary>
        public Int64 Line { get; private set; }

        /// <summary>
        /// Gets the column.
        /// </summary>
        public Int64 Column { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextPosition"/> struct.
        /// </summary>
        public TextPosition(Int64 line, Int64 column) : this()
        {
            Line = line;
            Column = column;
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public Boolean Equals(TextPosition other)
        {
            return Line == other.Line && Column == other.Column;
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <returns>
        /// true if <paramref name="obj"/> and this instance are the same type and represent the same value; otherwise, false.
        /// </returns>
        /// <param name="obj">Another object to compare to. </param>
        public override Boolean Equals(Object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is TextPosition && Equals((TextPosition) obj);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer that is the hash code for this instance.
        /// </returns>
        public override Int32 GetHashCode()
        {
            unchecked
            {
                return (Line.GetHashCode() * 397) ^ Column.GetHashCode();
            }
        }

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the <paramref name="other"/> parameter.Zero This object is equal to <paramref name="other"/>. Greater than zero This object is greater than <paramref name="other"/>. 
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public Int32 CompareTo(TextPosition other)
        {
            if (this.Equals(other)) return 0;

            if (this.Line < other.Line) return -1;
            if (this.Line > other.Line) return 1;
            if (this.Column < other.Column) return -1;

            return 1;
        }
    }

    public class TextSpan : IEquatable<TextSpan>
    {
        /// <summary>
        /// Gets or sets the start.
        /// </summary>
        public TextPosition Start { get; protected set; }

        /// <summary>
        /// Gets or sets the stop.
        /// </summary>
        public TextPosition Stop { get; protected set; }

        public TextSpan(TextPosition start, TextPosition stop)
        {
            if (start.CompareTo(stop) > 0)
                throw new ArgumentException("Start should be lesser than stop.");

            Start = start;
            Stop = stop;
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public Boolean Equals(TextSpan other)
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
            return Equals((TextSpan) obj);
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

        public Boolean IsOverlapping(TextSpan other)
        {
            if (this.Equals(other)) return true;

            return (Start.CompareTo(other.Start) >= 0 &&
                    Stop.CompareTo(other.Stop) <= 0) ||
                   (other.Start.CompareTo(Start) >= 0 &&
                    other.Stop.CompareTo(Stop) <= 0)
            ;
        }

        public Boolean IsSubRangeOf(TextSpan other)
        {
            if (this.Equals(other)) return true;

            return other.Start.CompareTo(Start) <= 0 &&
                   other.Stop.CompareTo(Stop) >= 0;
        }

        public Boolean IsSuperRangeOf(TextSpan other)
        {
            if (this.Equals(other)) return true;

            return Start.CompareTo(other.Start) <= 0 &&
                   Stop.CompareTo(other.Stop) >= 0;
        }
    }
}