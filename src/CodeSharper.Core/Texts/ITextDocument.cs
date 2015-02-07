using System;
using System.Text;

namespace CodeSharper.Core.Texts
{
    public interface ITextDocument
    {
        /// <summary>
        /// Gets or sets the text of document.
        /// </summary>
        StringBuilder Text { get; }

        /// <summary>
        /// Registers the specified text range of TextDocument. 
        /// </summary>
        void Register(TextRange textRange);

        /// <summary>
        /// Unregisters the specified text range in TextDocument
        /// </summary>
        void Unregister(TextRange textRange);

        /// <summary>
        /// Gets an existing or creates a new text range in text document
        /// </summary>
        TextRange GetOrCreateTextRange(Int32 start, Int32 stop);

        /// <summary>
        /// Updates the text of text document based on a specified text range
        /// </summary>
        TextDocument UpdateText(Int32 oldStart, Int32 oldStop, TextRange updatedTextRange);
    }
}