using System;
using System.Text;

namespace CodeSharper.Core.Texts
{
    public interface ITextDocument
    {
        /// <summary>
        /// Gets or sets the text of document.
        /// </summary>
        String Text { get; }
    }
}