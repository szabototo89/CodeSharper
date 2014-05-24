using System;

namespace CodeSharper.Core
{
    public static class TextLocationHelper
    {
        /// <summary>
        /// Gets the distance from TextLocation.
        /// </summary>
        public static TextLocation GetDistanceFrom(this TextLocation that, TextLocation location)
        {
            var column = Math.Abs(location.Column - that.Column);
            var line = Math.Abs(location.Line - that.Line);

            return new TextLocation(column, line);
        }

        /// <summary>
        /// Returns a new TextLocation object which is offset by column and line.
        /// </summary>
        public static TextLocation Offset(TextLocation that, int offsetColumn, int offsetLine)
        {
            return new TextLocation(that.Column + offsetColumn, that.Line + offsetLine);
        }
    }
}