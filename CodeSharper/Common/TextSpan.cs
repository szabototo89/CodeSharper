using System;
using System.Globalization;
using System.Net.Mime;

namespace CodeSharper.Common
{
    public struct TextSpan
    {
        public TextPosition Start { get; private set; }
        public TextPosition End { get; private set; }
        public static readonly TextSpan Zero = new TextSpan();

        public TextSpan(TextPosition start, TextPosition end)
            : this()
        {
            Start = start;
            End = end;

            if (Start.CompareTo(End) > 0)
                throw new Exception("'Start' cannot be greater than 'End'!");
        }

        public override string ToString()
        {
            return string.Format("{0}-{1}", Start, End);
        }

        public static TextSpan GetFromString(string text)
        {
            return GetFromString(text, TextPosition.Zero);    
        }

        public static TextSpan GetFromString(string text, TextPosition start)
        {
            return new TextSpan(start,
                                start.OffsetByString(text));
        }

        public static TextSpan GetFromString(string text, int line, int charPosition)
        {
            return GetFromString(text, new TextPosition(line, charPosition));
        }
    }

    public static class TextSpanHelper
    {
    }
}