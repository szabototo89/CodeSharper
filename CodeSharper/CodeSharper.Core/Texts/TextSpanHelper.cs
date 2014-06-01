using CodeSharper.Core.Common;

namespace CodeSharper.Core.Texts
{
    public static class TextSpanHelper
    {
        #region Public methods

        public static TextSpan Append(this TextSpan that, TextSpan value)
        {
            return new TextSpan(that.Start, that.Text + value.Text);
        }

        public static TextSpan AppendTo(this TextSpan that, TextSpan value)
        {
            return value.Append(that);
        }

        public static TextSpan OffsetBy(this TextSpan that, int offset)
        {
            if (that.Start + offset < 0)
                throw ThrowHelper.InvalidOperationException();

            return new TextSpan(that.Start + offset, that.Text);
        }

        public static TextSpan Prepend(this TextSpan that, TextSpan value)
        {
            return new TextSpan(value.Start, value.Text + that.Text);
        }

        public static TextSpan PrependTo(this TextSpan that, TextSpan value)
        {
            return value.Prepend(that);
        }

        #endregion
    }
}