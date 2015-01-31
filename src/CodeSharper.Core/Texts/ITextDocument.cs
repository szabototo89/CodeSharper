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
    }
}