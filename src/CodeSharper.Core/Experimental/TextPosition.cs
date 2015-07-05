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
        /// Gets or sets the line.
        /// </summary>
        public Int32 Line { get; internal set; }

        /// <summary>
        /// Gets or sets the column.
        /// </summary>
        public Int32 Column { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextPosition"/> struct.
        /// </summary>
        public TextPosition(Int32 line, Int32 column) : this()
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
        public bool Equals(TextPosition other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Line == other.Line && Column == other.Column;
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified object  is equal to the current object; otherwise, false.
        /// </returns>
        /// <param name="obj">The object to compare with the current object. </param>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TextPosition) obj);
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return (Line * 397) ^ Column;
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

        /// <summary>
        /// Returns the fully qualified type name of this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> containing a fully qualified type name.
        /// </returns>
        public override String ToString()
        {
            return String.Format("Position(line: {0}, column: {1})", Line, Column);
        }

        public TextPosition Offset(Int32 line, Int32 column)
        {
            return new TextPosition(Line + line, Column + column);
        }
    }
}