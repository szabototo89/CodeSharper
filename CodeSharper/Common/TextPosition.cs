using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSharper.Common
{
    public struct TextPosition : IComparable<TextPosition>
    {
        public static readonly TextPosition Zero = new TextPosition(0, 0);

        public int Line { get; private set; }
        public int CharPosition { get; private set; }

        public TextPosition(int line, int charPosition)
            : this()
        {
            Line = line;
            CharPosition = charPosition;
        }

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the <paramref name="other"/> parameter.Zero This object is equal to <paramref name="other"/>. Greater than zero This object is greater than <paramref name="other"/>. 
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public int CompareTo(TextPosition other)
        {
            if (Line < other.Line)
                return -1;
            if (Line > other.Line)
                return 1;

            if (CharPosition < other.CharPosition)
                return -1;
            if (CharPosition > other.CharPosition)
                return 1;

            return 0;
        }

        public override string ToString()
        {
            return string.Format("({0}, {1})", Line, CharPosition);
        }
    }

    public static class TextPositionHelper
    {
        public static TextPosition Offset(this TextPosition that, TextPosition offset)
        {
            return new TextPosition(that.Line + offset.Line, that.CharPosition + offset.CharPosition);
        }

        public static TextPosition OffsetByCharPosition(this TextPosition that, int offsetCharPosition)
        {
            return that.Offset(new TextPosition(0, offsetCharPosition));
        }

        public static TextPosition OffsetByLinePosition(this TextPosition that, int offsetLine)
        {
            return that.Offset(new TextPosition(offsetLine, 0));
        }

        public static TextPosition OffsetByString(this TextPosition that, string text)
        {
            if (string.IsNullOrEmpty(text))
                return that;

            var lines = text.Split(new string[] { "\n", Environment.NewLine }, StringSplitOptions.None);

            if (!lines.Any())
                throw new ArgumentException("There is no line!", "text");

            return that.OffsetByLinePosition(lines.Length - 1)
                       .OffsetByCharPosition(lines.Last().Length);
        }
    }
}
