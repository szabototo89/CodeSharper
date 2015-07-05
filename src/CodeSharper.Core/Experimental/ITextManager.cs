using System;

namespace CodeSharper.Core.Experimental
{
    public interface ITextManager
    {
        /// <summary>
        /// Gets the value.
        /// </summary>
        String GetValue(TextSpan span);

        /// <summary>
        /// Sets the value.
        /// </summary>
        void SetValue(String value, TextSpan span);

        /// <summary>
        /// Creates the or get text span.
        /// </summary>
        TextSpan CreateOrGetTextSpan(TextPosition inclusiveStart, TextPosition exclusiveStop);
    }
}