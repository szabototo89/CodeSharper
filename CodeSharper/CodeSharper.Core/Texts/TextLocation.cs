using System;
using CodeSharper.Core.Common;

namespace CodeSharper.Core.Texts
{
    public struct TextLocation : IComparable, IComparable<TextLocation>
    {
        private readonly int _column;
        private readonly int _line;
        private readonly int _index;
        private static readonly TextLocation _zero;

        static TextLocation()
        {
            _zero = new TextLocation(0, 0);
        }

        public TextLocation(int column = 0, int line = 0, int index = 0)
        {
            if (column < 0)
                throw ThrowHelper.ArgumentException("Column must be positive number or zero!", "column");
            if (line < 0)
                throw ThrowHelper.ArgumentException("Line must be positive number or zero!", "line");

            _column = column;
            _line = line;
            _index = index;
        }

        /// <summary>
        /// Returns column number of text
        /// </summary>
        public int Column
        {
            get { return _column; }
        }

        /// <summary>
        /// Returns line number of text
        /// </summary>
        public int Line
        {
            get { return _line; }
        }

        /// <summary>
        /// Returns zero text location
        /// </summary>
        public static TextLocation Zero
        {
            get { return _zero; }
        }

        public int Index
        {
            get { return _index; }
        }

        #region IComparable interface

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has these meanings: Value Meaning Less than zero This instance precedes <paramref name="obj"/> in the sort order. Zero This instance occurs in the same position in the sort order as <paramref name="obj"/>. Greater than zero This instance follows <paramref name="obj"/> in the sort order. 
        /// </returns>
        /// <param name="obj">An object to compare with this instance. </param><exception cref="T:System.ArgumentException"><paramref name="obj"/> is not the same type as this instance. </exception><filterpriority>2</filterpriority>
        int IComparable.CompareTo(object obj)
        {
            if (obj == null)
                return 1;

            if (obj is TextLocation)
                return CompareTo((TextLocation)obj);

            throw ThrowHelper.ArgumentException("obj", "Must be TextLocation object!");
        }

        #endregion

        #region IComparable(T) interface

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the <paramref name="other"/> parameter.Zero This object is equal to <paramref name="other"/>. Greater than zero This object is greater than <paramref name="other"/>. 
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public int CompareTo(TextLocation other)
        {
            if (Column < other.Column)
                return -1;
            if (Column > other.Column)
                return 1;
            if (Line < other.Line)
                return -1;
            if (Line > other.Line)
                return 1;

            return 0;
        }

        #endregion

        /// <summary>
        /// Returns the fully qualified type name of this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> containing a fully qualified type name.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return string.Format("Line {0} - Column {1} - Index {2}", Line, Column, Index);
        }
    }
}
