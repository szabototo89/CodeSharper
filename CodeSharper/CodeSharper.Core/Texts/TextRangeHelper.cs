using System.Net;
using CodeSharper.Core.Common;

namespace CodeSharper.Core.Texts
{
    public static class TextRangeHelper
    {
        #region Public methods

        public static TextRange Append(this TextRange that, TextRange value)
        {
            var text = that.Text;
            var distance = value.Start - (that.Start + text.Length);
            if (distance > 0)
                text += new string('\0', distance);
            text += value.Text;

            return new TextRange(that.Start, text);
        }

        public static TextRange AppendTo(this TextRange that, TextRange value)
        {
            return value.Append(that);
        }

        public static TextRange OffsetBy(this TextRange that, int offset)
        {
            if (that.Start + offset < 0)
                throw ThrowHelper.InvalidOperationException();

            return new TextRange(that.Start + offset, that.Text);
        }

        public static TextRange Prepend(this TextRange that, TextRange value)
        {
            return new TextRange(value.Start, value.Text + that.Text);
        }

        public static TextRange PrependTo(this TextRange that, TextRange value)
        {
            return value.Prepend(that);
        }

        #endregion
    }
}