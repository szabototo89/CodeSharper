using System.Net;
using CodeSharper.Core.Common;

namespace CodeSharper.Core.Texts
{
    public static class TextRangeHelper
    {
        #region Public methods

        public static TextRange OffsetBy(this TextRange that, int offset)
        {
            if (that.Start + offset < 0)
                throw ThrowHelper.InvalidOperationException();

            return new TextRange(that.Start + offset, that.Text);
        }

        #endregion
    }
}