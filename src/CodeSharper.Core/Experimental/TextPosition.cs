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
        public Int32 Line { get; private set; }

        /// <summary>
        /// Gets the column.
        /// </summary>
        public Int32 Column { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextPosition"/> struct.
        /// </summary>
        public TextPosition(Int32 line, Int32 column)
            : this()
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
}