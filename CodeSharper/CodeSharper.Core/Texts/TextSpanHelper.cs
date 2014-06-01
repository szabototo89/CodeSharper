using CodeSharper.Core.Common;

namespace CodeSharper.Core.Texts
{
    public static class TextSpanHelper
    {
        #region Public methods

        public static TextSpan OffsetBy(this TextSpan that, int offset)
        {
            if (that.Start + offset < 0)
                throw ThrowHelper.InvalidOperationException();

            return new TextSpan(that.Start + offset, that.Text);
        }

        #endregion
    }
}